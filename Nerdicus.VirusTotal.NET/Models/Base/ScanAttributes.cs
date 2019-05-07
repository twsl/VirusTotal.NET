using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Converters;
using Nerdicus.VirusTotalNET.Models.Base;
using Nerdicus.VirusTotalNET.Models.File.Analysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nerdicus.VirusTotalNET.Models.Base
{
    public class ScanAttributes : Attributes
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("results")]
        public Dictionary<string, ScanEngine> Results { get; set; }

        [JsonProperty("stats")]
        public LastAnalysisStats Stats { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(ScanFileResponseStatusCodeConverter))]
        public ScanFileResponseStatusCode Status { get; set; }
    }
}
