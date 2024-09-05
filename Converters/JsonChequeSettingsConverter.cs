using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class JsonChequeSettingsConverter : JsonConverter<ChequeSettings>
    {
        public override ChequeSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            List<BaseCheque> cheques = root.GetProperty("chequeList")
                .EnumerateArray()
                .Select(chequeElement =>
                {
                    return chequeElement.Deserialize<BaseCheque>(options) ?? throw new JsonException($"Invalid cheque list JSON.");
                })
                .ToList();

            return ChequeSettings.Create(cheques);
        }

        public override void Write(Utf8JsonWriter writer, ChequeSettings value, JsonSerializerOptions options)
        {
            // Start writing the cheque list
            writer.WriteStartObject();

            // Write the chequeList property
            writer.WritePropertyName("chequeList");

            // Start writing the cheque array
            writer.WriteStartArray();

            // Write each cheque inside
            foreach (BaseCheque cheque in value.ChequeList)
            {
                JsonSerializer.Serialize(writer, cheque, options);
            }

            // End writing the array
            writer.WriteEndArray();

            // End writing the whole list
            writer.WriteEndObject();
        }
    }
}
