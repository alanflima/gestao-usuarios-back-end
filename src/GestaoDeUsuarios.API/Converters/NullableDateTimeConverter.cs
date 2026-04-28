using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestaoDeUsuarios.API.Converters;

public class NullableDateTimeConverter : JsonConverter<DateTime?>
{
    private const string Format = "dd/MM/yyyy HH:mm:ss";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString();
        return s is null ? null : DateTime.ParseExact(s, Format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(Format));
        else
            writer.WriteNullValue();
    }
}
