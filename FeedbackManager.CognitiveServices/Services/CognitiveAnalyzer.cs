using Azure.AI.TextAnalytics;
using Azure;
using FeedbackManager.CognitiveServices.Configuration;
using FeedbackManager.Core.Entities;
using FeedbackManager.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace FeedbackManager.CognitiveServices.Services
{
    /// <summary>
    ///  More info about samples here:
    ///  https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/textanalytics/Azure.AI.TextAnalytics/samples/README.md
    /// </summary>
    public class CognitiveAnalyzer : IFeedbackAnalyzer
    {
        private readonly CognitiveAnalyzerConfiguration configuration;
        private readonly ILogger<CognitiveAnalyzer> logger;

        public CognitiveAnalyzer(IConfiguration configuration,ILogger<CognitiveAnalyzer> logger)
        {
            this.configuration=new CognitiveAnalyzerConfiguration(configuration);
            this.logger = logger;

            this.configuration.Load();
        }

        public async Task<FeedbackReport> AnalyzeAsync(FeedbackData feedback, CancellationToken token = default)
        {
            var response = new FeedbackReport(feedback);

            Uri endpoint = new(this.configuration.Endpoint);
            AzureKeyCredential credential = new(this.configuration.ApiKey);
            TextAnalyticsClient client = new(endpoint, credential);
            
            try
            {
                Response<DetectedLanguage> languageResponse = 
                    await client.DetectLanguageAsync(feedback.Text);
               
                response.Language = languageResponse.Value.Name;

                AnalyzeSentimentOptions options = new() { IncludeOpinionMining = true };
                Response<DocumentSentiment> sentimentResponse = 
                    await client.AnalyzeSentimentAsync(feedback.Text,
                        languageResponse.Value.Iso6391Name, options);

                response.Sentiment = sentimentResponse.Value.ToSentiment();
                response.SentimentConfidence = sentimentResponse.Value.GetConfidence();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex,"Exception during feedback analysis");
                throw;
            }
            return response;
        }
    }
}
