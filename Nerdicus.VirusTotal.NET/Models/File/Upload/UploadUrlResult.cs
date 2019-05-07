using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File.Upload
{
    public class UploadUrlResult
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
