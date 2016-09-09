using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public class Feature
    {
        public string Label { get; set; }
        public double[] Values { get; set; }
    }
}