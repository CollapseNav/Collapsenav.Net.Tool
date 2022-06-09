using System.Text.Json;
using System.Text.Json.Serialization;

namespace Collapsenav.Net.Tool;

public class SnowflakeConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString().IsNull())
            return default;
        else
            return long.TryParse(reader.GetString(), out long result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}