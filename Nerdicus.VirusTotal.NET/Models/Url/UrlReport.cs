using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Url
{
    public class UrlReport
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
