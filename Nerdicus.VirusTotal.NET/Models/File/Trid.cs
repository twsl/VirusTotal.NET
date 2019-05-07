using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class Trid
    {
        [JsonProperty("file_type")]
        public string FileType { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }
    }
}
