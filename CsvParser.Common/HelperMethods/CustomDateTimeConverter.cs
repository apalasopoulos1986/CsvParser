using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CsvParser.Common.HelperMethods
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "M/d/yyyy"; 
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string dateString = reader.GetString();
                if (DateTime.TryParseExact(dateString, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    return date;
                }
            }
            throw new JsonException("Invalid date format. The format should be M/d/yyyy");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format, CultureInfo.InvariantCulture));
        }
    }
}
