using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class FileReport
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
