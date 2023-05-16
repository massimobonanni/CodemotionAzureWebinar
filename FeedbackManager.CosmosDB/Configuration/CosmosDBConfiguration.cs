using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.CosmosDB.Configuration
{
    internal class CosmosDBConfiguration
    {
        public CosmosDBConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly IConfiguration configuration;

        public string? Endpoint { get; set; }
        public string? AccessKey { get; set; }
        public string? DatabaseName { get; set; } = "feedbackManager";
        public string? ContainerName { get; set; } = "feedback";

        public void Load()
        {
            Endpoint = configuration["CosmosDB:Endpoint"];
            AccessKey = configuration["CosmosDB:AccessKey"];
            if (configuration["CosmosDB:DatabaseName"]!=null)
            {
                DatabaseName = configuration["CosmosDB:DatabaseName"];
            }
            if (configuration["CosmosDB:ContainerName"] != null)
            {
                ContainerName = configuration["CosmosDB:ContainerName"];
            }
        }
    }
}
