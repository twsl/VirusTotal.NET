using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nerdicus.VirusTotalNET.Models.Url
{
    public class Attributes : Base.Attributes
    {
        [JsonProperty("categories")]
        public Dictionary<string,string> Categories { get; set; }

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

        [JsonProperty("last_final_url")]
        public string LastFinalUrl { get; set; }

        [JsonProperty("last_http_response_code")]
        public int LastHttpResponseCode { get; set; }

        [JsonProperty("last_http_response_content_length")]
        public int LastHttpResponseContentLength { get; set; }

        [JsonProperty("last_http_response_content_sha256")]
        public string LastHttpResponseContentSha256 { get; set; }

        [JsonProperty("last_http_response_headers")]
        public LastHttpResponseHeaders LastHttpResponseHeaders { get; set; }

        [JsonProperty("last_modification_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastModificationDate { get; set; }

        [JsonProperty("last_submission_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastSubmissionDate { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("tags")]
        public IList<object> Tags { get; set; }

        [JsonProperty("times_submitted")]
        public int TimesSubmitted { get; set; }

        [JsonProperty("total_votes")]
        public TotalVotes TotalVotes { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
