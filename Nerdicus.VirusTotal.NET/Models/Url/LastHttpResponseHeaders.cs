using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.Url
{
    public class LastHttpResponseHeaders
    {
        [JsonProperty("cache-control")]
        public string CacheControl { get; set; }

        [JsonProperty("content-length")]
        public string ContentLength { get; set; }

        [JsonProperty("content-type")]
        public string ContentType { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("expires")]
        public string Expires { get; set; }

        [JsonProperty("p3p")]
        public string P3p { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("set-cookie")]
        public string SetCookie { get; set; }

        [JsonProperty("x-frame-options")]
        public string XFrameOptions { get; set; }

        [JsonProperty("x-xss-protection")]
        public string XXssProtection { get; set; }
    }
}
