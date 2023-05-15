namespace FeedbackManager.Core.Entities
{
    public class FeedbackReport
    {
        public FeedbackReport(FeedbackData feedback)
        {
            this.Id=Guid.NewGuid();
            this.Feedback = feedback;
        }

        public FeedbackData Feedback { get; set; }
        public Guid Id { get; set; }
        public string? Language { get; set; }
        public Sentiment Sentiment { get; set; }
        public double SentimentConfidence { get; set; }
    }
}