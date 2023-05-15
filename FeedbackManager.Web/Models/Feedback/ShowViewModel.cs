using FeedbackManager.Core;
using System.ComponentModel.DataAnnotations;

namespace FeedbackManager.Web.Models.Feedback
{
    public class ShowViewModel
    {
        [Display(Name = "User")]
        public string? Username { get; set; }
        [Display(Name = "Feedback")] 
        public string? Text { get; set; }
        public string? Language { get; set; }
        public Sentiment Sentiment { get; set; }
        [Display(Name = "Confidence score")] 
        public double SentimentConfidence { get; set; }
    }
}
