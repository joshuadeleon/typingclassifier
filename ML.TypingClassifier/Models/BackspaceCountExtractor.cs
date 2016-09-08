using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public class BackspaceCountExtractor : IFeatureExtractor
    {
        public double Extract(Timeline timeline)
        {
            return timeline.Events.Where(e => e.Key == "Backspace").Count();
        }
    }
}