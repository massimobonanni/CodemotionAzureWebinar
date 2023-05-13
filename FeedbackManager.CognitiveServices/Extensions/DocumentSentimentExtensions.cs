using FeedbackManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.AI.TextAnalytics
{
    internal static class DocumentSentimentExtensions
    {
        public static Sentiment ToSentiment(this DocumentSentiment source)
        {
            switch (source.Sentiment)
            {
                case TextSentiment.Positive:
                    return Sentiment.Positive;
                case TextSentiment.Neutral:
                    return Sentiment.Neutral;
                case TextSentiment.Negative:
                    return Sentiment.Negative;
                case TextSentiment.Mixed:
                default:
                    return Sentiment.Unknown;
            }
        }

        public static double GetConfidence(this DocumentSentiment source)
        {
            switch (source.Sentiment)
            {
                case TextSentiment.Positive:
                    return source.ConfidenceScores.Positive;
                case TextSentiment.Neutral:
                    return source.ConfidenceScores.Neutral;
                case TextSentiment.Negative:
                    return source.ConfidenceScores.Negative;
                case TextSentiment.Mixed:
                default:
                    return 0;
            }
        }
    }
}
