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
        public string? DatabaseName { get; set; } = "feedbackManager";
        public string? ContainerName { get; set; } = "feedback";
        public string? ClientId { get; set; }
        public string? TenantId { get; set; }
        public string? ClientSecret { get; set; }


        public void Load()
        {
            Endpoint = configuration["CosmosDB:Endpoint"];
            ClientId = configuration["CosmosDB:ClientId"];
            TenantId = configuration["CosmosDB:TenantId"];
            ClientSecret = configuration["CosmosDB:ClientSecret"];
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
