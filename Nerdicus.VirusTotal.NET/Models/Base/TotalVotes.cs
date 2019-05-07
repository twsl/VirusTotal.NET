using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Base
{
    public class TotalVotes
    {
        [JsonProperty("harmless")]
        public int Harmless { get; set; }

        [JsonProperty("malicious")]
        public int Malicious { get; set; }
    }
}
