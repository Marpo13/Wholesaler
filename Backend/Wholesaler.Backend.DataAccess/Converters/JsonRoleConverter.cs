using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wholesaler.Backend.DataAccess.Converters;

internal class JsonRoleConverter : JsonConverter<Models.Helpers.Role>
{
    public override Models.Helpers.Role? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var document = JsonDocument.ParseValue(ref reader))
        {
            var root = document.RootElement;
            var typeProperty = root.GetProperty("Type").GetString();

            return typeProperty switch
            {
                "Manager" => JsonSerializer.Deserialize<Models.Helpers.Manager>(root.GetRawText(), options),
                "Owner" => JsonSerializer.Deserialize<Models.Helpers.Owner>(root.GetRawText(), options),
                "Employee" => JsonSerializer.Deserialize<Models.Helpers.Employee>(root.GetRawText(), options),
                _ => throw new NotSupportedException($"Type '{typeProperty}' is not supported")
            };
        }
    }

    public override void Write(Utf8JsonWriter writer, Models.Helpers.Role value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}