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
            bool showAmountsOnTv = root.GetProperty("showAmountsOnTv").GetBoolean();
            RoundingMethod roundingMethod = (RoundingMethod) root.GetProperty("roundingMethod").GetInt32();

            return GameSettings.Create(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts, showAmountsOnTv, roundingMethod);
        }

        public override void Write(Utf8JsonWriter writer, GameSettings value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("startEnvelopeCount", value.StartEnvelopeCount);
            writer.WriteNumber("decimalPlaces", value.DecimalPlaces);
            writer.WriteBoolean("onlyWorstMinusCounts", value.OnlyWorstMinusCounts);
            writer.WriteBoolean("onlyBestPlusCounts", value.OnlyBestPlusCounts);
            writer.WriteBoolean("showAmountsOnTv", value.ShowAmountsOnTv);
            writer.WriteNumber("roundingMethod", (int) value.Rounding);

            writer.WriteEndObject();
        }
    }
}
