using System.ComponentModel.DataAnnotations;

namespace FeedbackManager.Web.Models.Feedback
{
    public class NewViewModel
    {
        [Required()]
        public string Username { get; set; }

        [Required()]
        public string Text { get; set; }
    }
}
