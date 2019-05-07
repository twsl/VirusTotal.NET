using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class PeInfo
    {
        [JsonProperty("entry_point")]
        public int EntryPoint { get; set; }

        [JsonProperty("exports")]
        public IList<string> Exports { get; set; }

        [JsonProperty("imphash")]
        public string Imphash { get; set; }

        [JsonProperty("imports")]
        public Dictionary<string, IList<string>> Imports { get; set; }

        [JsonProperty("machine_type")]
        public int MachineType { get; set; }

        [JsonProperty("overlay")]
        public Overlay Overlay { get; set; }

        [JsonProperty("resource_details")]
        public IList<ResourceDetail> ResourceDetails { get; set; }

        [JsonProperty("resource_langs")]
        public Dictionary<string, int> ResourceLangs { get; set; }

        [JsonProperty("resource_types")]
        public Dictionary<string, int> ResourceTypes { get; set; }

        [JsonProperty("sections")]
        public IList<Section> Sections { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
