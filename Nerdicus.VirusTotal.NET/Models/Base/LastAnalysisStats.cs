using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Base
{
    public class LastAnalysisStats
    {
        [JsonProperty("failure")]
        public int Failure { get; set; }

        [JsonProperty("harmless")]
        public int Harmless { get; set; }

        [JsonProperty("malicious")]
        public int Malicious { get; set; }

        [JsonProperty("suspicious")]
        public int Suspicious { get; set; }

        [JsonProperty("timeout")]
        public int Timeout { get; set; }

        [JsonProperty("type-unsupported")]
        public int TypeUnsupported { get; set; }

        [JsonProperty("undetected")]
        public int Undetected { get; set; }
    }
}
