using System.ComponentModel.DataAnnotations;

namespace FeedbackManager.Web.Models.Feedback
{
    public class NewViewModel
    {
        public string? Username { get; set; }

        public bool IsLogged { get; set; } = false;

        [Required(ErrorMessage ="Feedback is mandatory")]
        public string? Text { get; set; }
    }
}
