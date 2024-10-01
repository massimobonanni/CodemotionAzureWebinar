using FeedbackManager.Core.Entities;
using FeedbackManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.Core.Implementations
{
    public class NoPersistanceService : IFeedbackPersistence
    {
        public Task<bool> InsertFeedbackReportAync(FeedbackReport feedbackReport, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}
