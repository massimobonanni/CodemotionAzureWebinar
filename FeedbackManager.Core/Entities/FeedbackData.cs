using System.Reflection.Metadata;

namespace FeedbackManager.Core.Entities
{
    public class FeedbackData
    {
        public FeedbackData(string user, string text)
        {
            this.User = user;
            this.Text = text;
            Timestamp = DateTimeOffset.Now;
        }

        public string User { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}