using System.ComponentModel.DataAnnotations;

namespace FeedbackManager.Web.Models.Feedback
{
    public class NewViewModel
    {
        public string? Username { get; set; }

        public bool IsLogged { get; set; } = false;

        public string? Text { get; set; }
    }
}
