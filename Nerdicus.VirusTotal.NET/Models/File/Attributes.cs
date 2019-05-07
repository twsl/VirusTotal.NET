using System;
using System.Collections.Generic;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class Attributes : Base.Attributes
    {
        [JsonProperty("authentihash")]
        public string AuthentiHash { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("exiftool")]
        public ExifTool ExifTool { get; set; }

        [JsonProperty("first_submission_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime FirstSubmissionDate { get; set; }

        [JsonProperty("last_analysis_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastAnalysisDate { get; set; }

        [JsonProperty("last_analysis_results")]
        public Dictionary<string, ScanEngine> LastAnalysisResults { get; set; }

        [JsonProperty("last_analysis_stats")]
        public LastAnalysisStats LastAnalysisStats { get; set; }

        [JsonProperty("last_modification_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastModificationDate { get; set; }

        [JsonProperty("last_submission_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastSubmissionDate { get; set; }

        [JsonProperty("magic")]
        public string Magic { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("meaningful_name")]
        public string MeaningfulName { get; set; }

        [JsonProperty("names")]
        public IList<string> Names { get; set; }

        [JsonProperty("pe_info")]
        public PeInfo PeInfo { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("signature_info")]
        public SignatureInfo SignatureInfo { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("ssdeep")]
        public string Ssdeep { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("times_submitted")]
        public int TimesSubmitted { get; set; }

        [JsonProperty("total_votes")]
        public TotalVotes TotalVotes { get; set; }

        [JsonProperty("trid")]
        public IList<Trid> Trid { get; set; }

        [JsonProperty("type_description")]
        public string TypeDescription { get; set; }

        [JsonProperty("type_tag")]
        public string TypeTag { get; set; }

        [JsonProperty("unique_sources")]
        public int UniqueSources { get; set; }

        [JsonProperty("vhash")]
        public string Vhash { get; set; }
    }
}
