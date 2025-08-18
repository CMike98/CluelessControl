using CluelessControl.Cheques;
using CluelessControl.Prizes;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class AllSettingsConverter : JsonConverter<AllSettings>
    {
        public override AllSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            PrizeList? prizeList = null;
            ChequeSettings chequeSettings;

            if (root.TryGetProperty("PrizeList", out JsonElement prizeListElement))
            {
                if (prizeListElement.ValueKind != JsonValueKind.Null)
                {
                    prizeList = prizeListElement.Deserialize<PrizeList>(options) ?? throw new JsonException($"Invalid prize list JSON.");
                }
            }

            if (root.TryGetProperty("ChequeSettings", out JsonElement chequeSettingsElement))
            {
                chequeSettings = chequeSettingsElement.Deserialize<ChequeSettings>(options) ?? throw new JsonException($"Invalid cheque settings JSON.");
            }
            else if (root.TryGetProperty("chequeList", out JsonElement _))
            {
                chequeSettings = root.Deserialize<ChequeSettings>(options) ?? throw new JsonException($"Invalid cheque settings JSON.");
            }
            else
            {
                throw new JsonException("Incorrect JSON file format.");
            }

            return AllSettings.Create(chequeSettings, prizeList);
        }

        public override void Write(Utf8JsonWriter writer, AllSettings value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            
            writer.WritePropertyName("PrizeList");
            if (value.PrizeList is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                JsonSerializer.Serialize(writer, value.PrizeList, value.PrizeList.GetType(), options);
            }

            writer.WritePropertyName("ChequeSettings");
            JsonSerializer.Serialize(writer, value.ChequeSettings, value.ChequeSettings!.GetType(), options);

            writer.WriteEndObject();
        }
    }
}
