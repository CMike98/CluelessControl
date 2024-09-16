using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class JsonGameSettingsConverter : JsonConverter<GameSettings>
    {
        public override GameSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            int startEnvelopeCount = root.GetProperty("startEnvelopeCount").GetInt32();
            int decimalPlaces = root.GetProperty("decimalPlaces").GetInt32();
            bool onlyWorstMinusCounts = root.GetProperty("onlyWorstMinusCounts").GetBoolean();
            bool onlyBestPlusCounts = root.GetProperty("onlyBestPlusCounts").GetBoolean();

            return GameSettings.Create(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts);
        }

        public override void Write(Utf8JsonWriter writer, GameSettings value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("startEnvelopeCount", value.StartEnvelopeCount);
            writer.WriteNumber("decimalPlaces", value.DecimalPlaces);
            writer.WriteBoolean("onlyWorstMinusCounts", value.OnlyWorstMinusCounts);
            writer.WriteBoolean("onlyBestPlusCounts", value.OnlyBestPlusCounts);

            writer.WriteEndObject();
        }
    }
}
