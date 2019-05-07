using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Url.Scan
{
    public class Data : Base.Data
    {
        [JsonProperty("attributes")]
        public ScanAttributes Attributes { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
