using FeedbackManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackManager.Web.Controllers
{
    public class ServiceInfoController : Controller
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceInfoController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            var feedbackAnalyzer = serviceProvider.GetService<IFeedbackAnalyzer>();
            this.ViewBag.FeedbackAnalyzer = feedbackAnalyzer?.GetType().FullName;
            var feedbackPersistence = serviceProvider.GetService<IFeedbackPersistence>();
            this.ViewBag.FeedbackPersistence = feedbackPersistence?.GetType().FullName;

            return View();
        }
    }
}
