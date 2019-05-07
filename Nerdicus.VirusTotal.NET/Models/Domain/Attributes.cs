using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdicus.VirusTotalNET.Models.Domain
{
    public class Attributes : Base.Attributes
    {
        [JsonProperty("categories")]
        public Dictionary<string,string> Categories { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("last_update_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastUpdateDate { get; set; }

        [JsonProperty("registrar")]
        public string Registrar { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("total_votes")]
        public TotalVotes TotalVotes { get; set; }

        [JsonProperty("whois")]
        public string Whois { get; set; }

        [JsonProperty("whois_date")]
        public int WhoisDate { get; set; }
    }
}
