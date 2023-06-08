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
        private readonly IFeedbackPersistence _feedbackPersistence;

        public FeedbackController(ILogger<FeedbackController> logger,
            IFeedbackAnalyzer feedbackAnalyzer,
            IFeedbackPersistence feedbackPersistence)
        {
            _logger = logger;
            _feedbackAnalyzer = feedbackAnalyzer;
            _feedbackPersistence = feedbackPersistence;
        }

        public ActionResult Show(ShowViewModel viewModel)
        {
            return View(viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new NewViewModel();
            viewModel.Username = Request.GetAADPrincipalName();
            viewModel.IsLogged = !string.IsNullOrWhiteSpace(viewModel.Username);
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(NewViewModel viewModel)
        {
            #region Check user input 
            var loggedUsername = Request.GetAADPrincipalName();
            if (!string.IsNullOrWhiteSpace(loggedUsername))
            {
                viewModel.Username = loggedUsername;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Username))
            {
                ModelState.AddModelError(nameof(viewModel.Username), "Username is mandatory");
            }
            if (string.IsNullOrWhiteSpace(viewModel.Text))
            {
                ModelState.AddModelError(nameof(viewModel.Text), "Feedback is mandatory");
            }
            #endregion

            if (ModelState.IsValid)
            {
                var feedbackdata = new FeedbackData(viewModel.Username, viewModel.Text);

                var analysisReport = await this._feedbackAnalyzer.AnalyzeAsync(feedbackdata);

                if (analysisReport != null)
                {
                    var insertResult = await this._feedbackPersistence.InsertFeedbackReportAync(analysisReport);
                    if (insertResult)
                    {
                        var showModel = new ShowViewModel()
                        {
                            Username = viewModel.Username,
                            Text = viewModel.Text,
                            Language = analysisReport.Language,
                            Sentiment = analysisReport.Sentiment,
                            SentimentConfidence = analysisReport.SentimentConfidence
                        };
                        return RedirectToAction(nameof(Show), showModel);
                    }
                }
            }

            return View(viewModel);
        }

    }
}
