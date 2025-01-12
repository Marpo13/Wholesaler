using System.Text.Json;
using System.Text.Json.Serialization;
using Wholesaler.Backend.DataAccess.Models.Helpers;

namespace Wholesaler.Backend.DataAccess.Converters;

internal class JsonRoleConverter : JsonConverter<Role>
{
    public override Role? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var document = JsonDocument.ParseValue(ref reader))
        {
            var root = document.RootElement;
            var typeProperty = root.GetProperty("Type").GetString();

            return typeProperty switch
            {
                "Manager" => JsonSerializer.Deserialize<Manager>(root.GetRawText(), options),
                "Owner" => JsonSerializer.Deserialize<Owner>(root.GetRawText(), options),
                "Employee" => JsonSerializer.Deserialize<Employee>(root.GetRawText(), options),
                _ => throw new NotSupportedException($"Type '{typeProperty}' is not supported")
            };
        }
    }

    public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
