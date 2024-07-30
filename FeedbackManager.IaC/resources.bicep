@description('The location wher you want to create the resources.')
param location string = resourceGroup().location

@description('The prefix of the environment. It will be used to create the name of the resources in the resource group.')
@maxLength(16)
@minLength(3)
param environmentPrefix string = 'FeedMng'

var environmentName = '${environmentPrefix}-${uniqueString(subscription().id, resourceGroup().name)}'

// Create AppService, plan and application insights
var appServiceName = '${environmentName}-app'
var appServicePlanName = '${environmentName}-plan'
var appInsightName = '${environmentName}-insight'

resource appServicePlan 'Microsoft.Web/serverfarms@2016-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource appService 'Microsoft.Web/sites@2022-09-01' = {
  name: appServiceName
  location: location
  kind: 'app'
  properties:{
    serverFarmId: appServicePlan.id
  }
  identity: {
    type: 'SystemAssigned'
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Request_Source: 'rest'
  }
}

resource appSettings 'Microsoft.Web/sites/config@2022-03-01' = {
  name: 'appsettings'
  parent: appService
  properties: {
    APPINSIGHTS_INSTRUMENTATIONKEY: appInsights.properties.InstrumentationKey
    APPLICATIONINSIGHTS_CONNECTION_STRING: appInsights.properties.ConnectionString
    ApplicationInsightsAgent_EXTENSION_VERSION: '~3'
    XDT_MicrosoftApplicationInsights_Mode : 'Recommended'
    'CognitiveServices:ApiKey':''
    'CognitiveServices:Endpoint':''
    'CosmosDB:AccessKey':''
    'CosmosDB:Endpoint':''
  }
}

// Create Cognitive Services
var cognitiveServiceName = '${environmentName}-cs'

resource cognitiveService 'Microsoft.CognitiveServices/accounts@2022-12-01' = {
  name: cognitiveServiceName
  location: location
  sku: {
    name: 'S0'
  }
  kind: 'CognitiveServices'
  properties: {
    apiProperties: {
      statisticsEnabled: false
    }
  }
}

// Create KeyVault
var keyVaultName = '${environmentName}-kv'

resource keyVault 'Microsoft.KeyVault/vaults@2023-02-01' = {
  name: keyVaultName
  location: location
  properties: {
    accessPolicies: []
    enableRbacAuthorization: true
    enableSoftDelete: false
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: false
    tenantId: subscription().tenantId
    sku: {
      name: 'standard'
      family: 'A'
    }
    networkAcls: {
      defaultAction: 'Allow'
      bypass: 'AzureServices'
    }
  }
}

// Create CosmosDB
var cosmosDBAccountName = toLower('${environmentName}-db')
var cosmosDBDatabaseName = 'feedbackManager'
var cosmosDBContainerName = 'feedback'

resource cosmosDBAccount 'Microsoft.DocumentDB/databaseAccounts@2023-04-15'={
  name: cosmosDBAccountName
  location: location
  kind: 'GlobalDocumentDB'
  properties:{
    publicNetworkAccess: 'Enabled'
    locations:[
      {
        locationName: location
        failoverPriority: 0
      }
    ]
    databaseAccountOfferType: 'Standard'
  }
}

resource cosmosDBDatabase 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2023-04-15' = {
  name: cosmosDBDatabaseName
  parent: cosmosDBAccount
  properties: {
    resource: {
      id: cosmosDBDatabaseName
    }
    options: {
       throughput: 400
    }
  }
}

resource cosmosDBContainer 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases/containers@2023-04-15' = {
  name: cosmosDBContainerName
  parent: cosmosDBDatabase
  properties: {
    resource: {
      id: cosmosDBContainerName
      indexingPolicy:{
        indexingMode: 'consistent'
        automatic: true
        includedPaths:[
          {
            path: '/*'
          }
        ]
        excludedPaths: [
          {
            path: '/"_etag"/?'
          }
        ]
      }
      partitionKey: {
        paths: [
          '/data'
        ]
        kind: 'Hash'
        version: 2
      }
    }
    options: {
      throughput: 400
    }
  }
}
