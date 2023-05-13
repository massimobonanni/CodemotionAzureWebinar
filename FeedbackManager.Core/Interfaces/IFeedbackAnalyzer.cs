using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedbackManager.Core.Entities;

namespace FeedbackManager.Core.Interfaces
{
    public interface IFeedbackAnalyzer
    {
        Task<FeedbackReport> AnalyzeAsync(FeedbackData feedback, CancellationToken token = default);
    }
}
