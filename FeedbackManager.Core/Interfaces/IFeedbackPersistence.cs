using FeedbackManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManager.Core.Interfaces
{
    public interface IFeedbackPersistence
    {
        Task<bool>  InsertFeedbackReportAync(FeedbackReport feedbackReport, CancellationToken cancellationToken = default(CancellationToken));
    }
}
