using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File.Analysis
{
    public class Meta
    {
        [JsonProperty("file_info")]
        public FileHashInfo FileInfo { get; set; }
    }

}
