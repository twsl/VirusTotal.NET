using System;
using Nerdicus.VirusTotalNET.Converters;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Base
{
    public class ScanEngine
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("engine_name")]
        public string EngineName { get; set; }

        [JsonProperty("engine_update")]
        [JsonConverter(typeof(YearMonthDayConverter))]
        public DateTime EngineUpdate { get; set; }

        [JsonProperty("engine_version")]
        public string EngineVersion { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }

    }
}
