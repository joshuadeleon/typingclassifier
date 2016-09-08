using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public class KeyEvent
    {
        [JsonProperty("e")]
        public string Key { get; set; }
        [JsonProperty("t")]
        public double Timestamp { get; set; }
        [JsonProperty("d")]
        public double Duration { get; set; }
    }
}