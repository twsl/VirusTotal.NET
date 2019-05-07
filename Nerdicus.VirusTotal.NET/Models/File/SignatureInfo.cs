using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class SignatureInfo
    {
        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("counter signers")]
        public string CounterSigners { get; set; }

        [JsonProperty("counter signers details")]
        public IList<CounterSignersDetail> CounterSignersDetails { get; set; }

        [JsonProperty("file version")]
        public string FileVersion { get; set; }

        [JsonProperty("original name")]
        public string OriginalName { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("signers")]
        public string Signers { get; set; }

        [JsonProperty("signers details")]
        public IList<SignersDetail> SignersDetails { get; set; }

        [JsonProperty("signing date")]
        public string SigningDate { get; set; }

        [JsonProperty("verified")]
        public string Verified { get; set; }

        [JsonProperty("x509")]
        public IList<X509> X509 { get; set; }
    }
}
