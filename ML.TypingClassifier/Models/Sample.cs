using Newtonsoft.Json;

namespace ML.TypingClassifier.Models
{
    public class Sample
    {
        [JsonProperty("handle")]
        public string Handle { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("events")]
        public KeyEvent[] Events { get; set; }
    }
}