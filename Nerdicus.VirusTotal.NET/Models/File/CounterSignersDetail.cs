using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class CounterSignersDetail
    {
        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("cert issuer")]
        public string CertIssuer { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("serial number")]
        public string SerialNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("thumbprint")]
        public string Thumbprint { get; set; }

        [JsonProperty("valid from")]
        public string ValidFrom { get; set; }

        [JsonProperty("valid to")]
        public string ValidTo { get; set; }

        [JsonProperty("valid usage")]
        public string ValidUsage { get; set; }
    }
}
