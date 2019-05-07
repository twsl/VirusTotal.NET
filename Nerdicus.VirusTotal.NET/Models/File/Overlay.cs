using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class Overlay
    {
        [JsonProperty("chi2")]
        public double Chi2 { get; set; }

        [JsonProperty("entropy")]
        public double Entropy { get; set; }

        [JsonProperty("filetype")]
        public string Filetype { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
