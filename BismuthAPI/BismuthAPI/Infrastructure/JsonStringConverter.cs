using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BismuthAPI.Infrastructure;

internal sealed class JsonStringConverter : JsonConverter<string?>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() is string value ? Regex.Replace(value.Trim(), @"\s+", " ") : null;

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        => writer.WriteStringValue(value);
}