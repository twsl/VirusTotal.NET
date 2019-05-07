using System;
using System.Collections.Generic;
using System.Text;
using Nerdicus.VirusTotalNET.Models.File.Analysis;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Converters
{
    public class ScanFileResponseStatusCodeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var status = (ScanFileResponseStatusCode)value;

            switch (status)
            {
                case ScanFileResponseStatusCode.Queued:
                    writer.WriteValue("queued");
                    break;
                case ScanFileResponseStatusCode.InProgress:
                    writer.WriteValue("in-progress");
                    break;
                case ScanFileResponseStatusCode.Completed:
                    writer.WriteValue("completed");
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string enumString = (string)reader.Value;
            switch (enumString)
            {
                case "queued":
                    return ScanFileResponseStatusCode.Queued;
                case "in-progress":
                    return ScanFileResponseStatusCode.InProgress;
                case "completed":
                    return ScanFileResponseStatusCode.Completed;
            }
            return null;
            //return Enum.Parse(typeof(AnalysisStatus), enumString, true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}