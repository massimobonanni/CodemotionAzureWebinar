using FeedbackManager.Core;
using FeedbackManager.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.CosmosDB.Entities
{
    internal class FeedbackReportItem
    {
        public FeedbackReportItem()
        {

        }

        public FeedbackReportItem(FeedbackReport feedbackReport)
        {
            this.Feedback=feedbackReport.Feedback;
            this.Id=feedbackReport.Id;
            this.Language=feedbackReport.Language;
            this.SentimentConfidence=feedbackReport.SentimentConfidence;
            this.Sentiment=feedbackReport.Sentiment;
            this.Data=$"{feedbackReport.Feedback.Timestamp:yyyyMMdd}";
        }

        public FeedbackData Feedback { get; set; }
        [JsonProperty(PropertyName ="id")]
        public Guid Id { get; set; }
        public string? Language { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Sentiment Sentiment { get; set; }
        public double SentimentConfidence { get; set; }
        public string Data { get; set; }
    }
}
