namespace FeedbackManager.Core.Entities
{
    public class FeedbackReport
    {
        public FeedbackReport(FeedbackData feedback)
        {
            this.Feedback = feedback;
        }

        public FeedbackData Feedback { get; set; }

        public string? Language { get; set; }
        public Sentiment Sentiment { get; set; }
        public decimal SentimentConfidence { get; set; }
    }
}