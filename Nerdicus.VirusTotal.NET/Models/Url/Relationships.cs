using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Url
{
    public class Relationships
    {
        [JsonProperty("last_serving_ip_address")]
        public Data LastServingIpAddress { get; set; }

        [JsonProperty("network_location")]
        public Data NetworkLocation { get; set; }
    }
}
