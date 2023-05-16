using FeedbackManager.Core;
using FeedbackManager.Core.Entities;
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
            this.Data=$"{feedbackReport.Feedback.Timestamp:yyymmdd}";
        }

        public FeedbackData Feedback { get; set; }
        public Guid Id { get; set; }
        public string? Language { get; set; }
        public Sentiment Sentiment { get; set; }
        public double SentimentConfidence { get; set; }
        public string Data { get; set; }
    }
}
