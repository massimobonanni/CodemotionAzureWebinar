using Azure.Identity;
using FeedbackManager.Core.Entities;
using FeedbackManager.Core.Interfaces;
using FeedbackManager.CosmosDB.Configuration;
using FeedbackManager.CosmosDB.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.CosmosDB.Services
{
    public class CosmosDBFeedbackPersistance : IFeedbackPersistence
    {
        private readonly CosmosDBConfiguration configuration;
        private readonly ILogger<CosmosDBFeedbackPersistance> logger;

        public CosmosDBFeedbackPersistance(IConfiguration configuration, ILogger<CosmosDBFeedbackPersistance> logger)
        {
            this.configuration = new CosmosDBConfiguration(configuration);
            this.logger = logger;

            this.configuration.Load();
        }

        public async Task<bool> InsertFeedbackReportAync(FeedbackReport feedbackReport, CancellationToken cancellationToken = default)
        {
            var response = true;

            var reportItem = new FeedbackReportItem(feedbackReport);
            try
            {
                var options = new CosmosClientOptions()
                {
                    ApplicationName = "FeedbackManager",
                    ApplicationRegion = Regions.FranceCentral
                };

                var client = new CosmosClient(this.configuration.Endpoint,
                    this.configuration.AccessKey);
                
                var database = client.GetDatabase(this.configuration.DatabaseName);
                
                var container = database.GetContainer(this.configuration.ContainerName);
                
                var insertedItem = await container.CreateItemAsync(reportItem, 
                    new PartitionKey(reportItem.Data),
                    null, 
                    cancellationToken);

                LogResponse(insertedItem);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error inserting feedback report");
                throw;
            }
            return response;
        }

        private void LogResponse<T>(ItemResponse<T> response)
        {
            this.logger.LogInformation("Activity id {0}", response.ActivityId);
            this.logger.LogInformation("Request charge {0} RU",response.RequestCharge);
            this.logger.LogInformation("Contacted regions: {0}",
                string.Join(";", response.Diagnostics.GetContactedRegions().Select(i => i.regionName)));
        }
    }
}
