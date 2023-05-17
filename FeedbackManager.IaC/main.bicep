targetScope = 'subscription'

@description('The name of the resource group that contains all the resources.')
param resourceGroupName string = 'FeedbackManager-rg'

@description('The location of the resource group and resources')
param location string = deployment().location

var environmentPrefix = 'FeedMng'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}

module resourcesModule 'resources.bicep' = {
  scope: resourceGroup
  name: 'resources'
  params: {
    location: location
    environmentPrefix: environmentPrefix
  }
}

