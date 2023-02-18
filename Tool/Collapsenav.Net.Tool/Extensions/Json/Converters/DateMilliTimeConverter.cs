using System.Text.Json;
using System.Text.Json.Serialization;

namespace Collapsenav.Net.Tool;

/// <summary>
/// 默认的时间转换
/// </summary>
/// <remarks>默认情况下将 DateTime 与 yyyy-MM-dd HH:mm:ss.fff 互相转换</remarks>
public class DateMilliTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString().IsNull())
            return default;
        else
            return DateTime.TryParse(reader.GetString(), out DateTime result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToDefaultMilliString());
    }
}