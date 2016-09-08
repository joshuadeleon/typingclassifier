using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public class WordsPerMinuteExtractor : IFeatureExtractor
    {
        public double Extract(Timeline timeline)
        {
            // Words per minute is essentially the number of spaces + 1 
            // divided by the total time. This is not perfect, but works
            // well for our purposes.
            var spaces = timeline.Events.Where(e => e.Key == " ").Count();
            var totalTime = timeline.Events.Last().Timestamp;
            return (double)(spaces + 1) / totalTime;
        }
    }
}