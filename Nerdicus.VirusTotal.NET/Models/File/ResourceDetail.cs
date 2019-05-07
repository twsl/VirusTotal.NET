using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class ResourceDetail
    {
        [JsonProperty("chi2")]
        public double Chi2 { get; set; }

        [JsonProperty("entropy")]
        public double Entropy { get; set; }

        [JsonProperty("filetype")]
        public string Filetype { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
