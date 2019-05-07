using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Nerdicus.VirusTotalNET.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nerdicus.VirusTotalNET.Converters
{
    public class YearMonthDayConverter : DateTimeConverterBase
    {
        private readonly CultureInfo _culture = new CultureInfo("en-us");
        private const string _dateTimeFormat = "yyyyMMdd";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.DateFormatString = _dateTimeFormat;
            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return DateTime.MinValue;

            if (reader.Value is string stringVal)
            {
                if (!ResourcesHelper.IsNumeric(stringVal))
                    return DateTime.MinValue;

                if (DateTime.TryParseExact(stringVal, _dateTimeFormat, _culture, DateTimeStyles.AllowWhiteSpaces, out DateTime result))
                    return result;
            }
            throw new FormatException("Invalid DateTime format.");
        }
    }
}
