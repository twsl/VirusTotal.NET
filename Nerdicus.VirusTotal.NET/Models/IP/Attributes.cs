using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdicus.VirusTotalNET.Models.IP
{
    public class Attributes : Base.Attributes
    {
        [JsonProperty("as_owner")]
        public string AsOwner { get; set; }

        [JsonProperty("asn")]
        public int Asn { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("regional_internet_registry")]
        public string RegionalInternetRegistry { get; set; }

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
