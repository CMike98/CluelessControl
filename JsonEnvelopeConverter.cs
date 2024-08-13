using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl
{
    public class JsonEnvelopeConverter : JsonConverter<BaseEnvelope>
    {
        public override BaseEnvelope? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            if (!root.TryGetProperty("type", out var typeProp))
                throw new JsonException("Envelope type is missing");
            if (typeProp.ValueKind != JsonValueKind.String)
                throw new JsonException("Envelope type is not a string");
            string envelopeType = typeProp.GetString() ?? throw new JsonException("Envelope type is missing");

            if (!root.TryGetProperty("value", out var valueProp))
                throw new JsonException("Envelope value is missing");

            return EnvelopeFactory.CreateEnvelope(envelopeType, valueProp);
        }

        public override void Write(Utf8JsonWriter writer, BaseEnvelope value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", value.GetType().Name.ToLowerInvariant());
            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
            writer.WriteEndObject();
        }
    }
}
