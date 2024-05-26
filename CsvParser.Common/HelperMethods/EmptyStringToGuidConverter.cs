using System.Text.Json;
using System.Text.Json.Serialization;

namespace CsvParser.Common.HelperMethods
{
    public class EmptyStringToGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.GetString() == string.Empty)
            {
                return Guid.Empty;
            }

            return reader.GetGuid();
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
