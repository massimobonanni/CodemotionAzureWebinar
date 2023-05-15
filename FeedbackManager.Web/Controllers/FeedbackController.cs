using FeedbackManager.Core.Entities;
using FeedbackManager.Core.Interfaces;
using FeedbackManager.Web.Models.Feedback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace FeedbackManager.Web.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IFeedbackAnalyzer _feedbackAnalyzer;

        public FeedbackController(ILogger<FeedbackController> logger,
            IFeedbackAnalyzer feedbackAnalyzer)
        {
            _logger = logger;
            _feedbackAnalyzer = feedbackAnalyzer;
        }

        public ActionResult Show(ShowViewModel viewModel)
        {
            return View(viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new NewViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(NewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var feedbackdata= new FeedbackData(viewModel.Username,viewModel.Text);

                var result=await this._feedbackAnalyzer.AnalyzeAsync(feedbackdata);

                var showModel=new ShowViewModel()
                {
                    Username=viewModel.Username,
                    Text=viewModel.Text,
                    Language=result.Language,
                    Sentiment=result.Sentiment,
                    SentimentConfidence=result.SentimentConfidence
                };

                return  RedirectToAction(nameof(Show),showModel);
            }

            return View(viewModel);
        }

    }
}
