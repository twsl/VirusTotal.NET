using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Url.Scan
{
    public class Meta
    {
        [JsonProperty("file_analysis_info")]
        public FileAnalysisInfo FileAnalysisInfo { get; set; }

        [JsonProperty("file_info")]
        public FileHashInfo FileInfo { get; set; }

        [JsonProperty("url_info")]
        public UrlInfo UrlInfo { get; set; }
    }
}
