using FeedbackManager.Core.Entities;
using FeedbackManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.CognitiveServices.Services
{
    public class CognitiveAnalyzer : IFeedbackAnalyzer
    {

        public Task<FeedbackReport> AnalyzeAsync(FeedbackData feedback, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
