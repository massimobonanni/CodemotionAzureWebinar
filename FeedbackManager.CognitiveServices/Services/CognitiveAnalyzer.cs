using FeedbackManager.CognitiveServices.Configuration;
using FeedbackManager.Core.Entities;
using FeedbackManager.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.CognitiveServices.Services
{
    /// <summary>
    ///  More info about samples here:
    ///  https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/textanalytics/Azure.AI.TextAnalytics/samples/README.md
    /// </summary>
    public class CognitiveAnalyzer : IFeedbackAnalyzer
    {
        private readonly CognitiveAnalyzerConfiguration configuration;

        public CognitiveAnalyzer(IConfiguration configuration)
        {
            this.configuration=new CognitiveAnalyzerConfiguration(configuration);

            this.configuration.Load();
        }

        public Task<FeedbackReport> AnalyzeAsync(FeedbackData feedback, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
