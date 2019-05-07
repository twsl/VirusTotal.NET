using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Base
{
    public class Links
    {
        [JsonProperty("related")]
        public string Related { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }
}
