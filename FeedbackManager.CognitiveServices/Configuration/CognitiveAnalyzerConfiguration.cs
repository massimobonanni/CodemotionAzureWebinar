using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.CognitiveServices.Configuration
{
    internal class CognitiveAnalyzerConfiguration
    {
        public CognitiveAnalyzerConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly IConfiguration configuration;

        public string? ApiKey { get; set; }
        public string? Endpoint { get; set; }

        public void Load()
        {
            ApiKey = configuration["CognitiveServices:ApiKey"];
            Endpoint = configuration["CognitiveServices:Endpoint"];
        }
    }
}
