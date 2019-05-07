using System.Collections.Generic;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Url.Scan
{
    public class UrlScanResult
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
