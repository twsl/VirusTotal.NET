using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdicus.VirusTotalNET.Models.Domain
{
    public class DomainReport
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
