using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class Section
    {
        [JsonProperty("entropy")]
        public double Entropy { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("raw_size")]
        public int RawSize { get; set; }

        [JsonProperty("virtual_address")]
        public int VirtualAddress { get; set; }

        [JsonProperty("virtual_size")]
        public int VirtualSize { get; set; }
    }
}
