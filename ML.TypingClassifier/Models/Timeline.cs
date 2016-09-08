using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public class Timeline
    {
        [JsonProperty("events")]
        public KeyEvent[] Events { get; set; }
    }
}