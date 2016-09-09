using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public class Sentinel
    {
        public string Author { get; set; }
        public string Book { get; set; }
        public string Text { get; set; }
        public int Length { get { return Text?.Length ?? 0; } }
    }
}