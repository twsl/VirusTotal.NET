using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdicus.VirusTotalNET.Models.Domain
{
    public class Data : Base.Data
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }
}
