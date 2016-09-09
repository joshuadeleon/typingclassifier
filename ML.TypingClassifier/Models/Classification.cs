using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accord.MachineLearning;

namespace ML.TypingClassifier.Models
{
    public class Classification
    {
        public KMeansClusterCollection Clusters { get; internal set; }
    }
}