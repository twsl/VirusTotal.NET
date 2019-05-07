using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdicus.VirusTotalNET.Models.IP
{
    public class IPReport
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
