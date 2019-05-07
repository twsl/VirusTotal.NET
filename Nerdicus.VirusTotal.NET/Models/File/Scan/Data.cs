using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File.Analysis
{
    public class Data : Base.Data
    {
        [JsonProperty("attributes")]
        public ScanAttributes Attributes { get; set; }
    }
}
