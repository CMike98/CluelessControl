using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class JsonChequeConverter : JsonConverter<BaseCheque>
    {
        public override BaseCheque? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            // Check the envelope type
            if (!root.TryGetProperty("type", out var typeProp))
                throw new JsonException("Envelope type is missing");
            if (typeProp.ValueKind != JsonValueKind.String)
                throw new JsonException("Envelope type is not a string");
            string envelopeType = typeProp.GetString() ?? throw new JsonException("Envelope type is missing");

            // Check the envelope value
            if (!root.TryGetProperty("value", out var valueProp))
                throw new JsonException("Envelope value is missing");

            // Create the envelope
            return ChequeFactory.CreateCheque(envelopeType, valueProp);
        }

        public override void Write(Utf8JsonWriter writer, BaseCheque value, JsonSerializerOptions options)
        {
            // Start writing the object
            writer.WriteStartObject();

            // Write the envelope type
            writer.WriteString("type", value.GetType().Name.ToLowerInvariant());

            // Write the envelope value
            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value, value.GetType(), options);

            // End writing the object
            writer.WriteEndObject();
        }
    }
}
