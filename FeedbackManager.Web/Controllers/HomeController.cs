using FeedbackManager.Core.Interfaces;
using FeedbackManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FeedbackManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedbackAnalyzer _feedbackAnalyzer;

        public HomeController(ILogger<HomeController> logger,
            IFeedbackAnalyzer feedbackAnalyzer)
        {
            _logger = logger;
            _feedbackAnalyzer = feedbackAnalyzer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}