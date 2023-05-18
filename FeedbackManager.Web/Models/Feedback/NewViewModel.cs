using System.ComponentModel.DataAnnotations;

namespace FeedbackManager.Web.Models.Feedback
{
    public class NewViewModel
    {
        [Required()]
        public string Username { get; set; }

        public bool IsLogged { get; set; } = false;

        [Required()]
        public string Text { get; set; }
    }
}
