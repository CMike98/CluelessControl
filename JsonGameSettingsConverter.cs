using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl
{
    public class JsonGameSettingsConverter : JsonConverter<GameSettings>
    {
        public override GameSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            int decimalPlaces = root.GetProperty("decimalPlaces").GetInt32();
            bool multipleMinusesAccumulate = root.GetProperty("multipleMinusesAccumulate").GetBoolean();
            bool fireworksActive = root.GetProperty("fireworksActive").GetBoolean();
            decimal minimumCashPrizeForFireworks = root.GetProperty("minimumCashPrizeForFireworks").GetDecimal();

            return GameSettings.Create(decimalPlaces, multipleMinusesAccumulate, fireworksActive, minimumCashPrizeForFireworks);
        }

        public override void Write(Utf8JsonWriter writer, GameSettings value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("decimalPlaces", value.DecimalPlaces);
            writer.WriteBoolean("multipleMinusesAccumulate", value.MultipleMinusesAccumulate);
            writer.WriteBoolean("fireworksActive", value.FireworksActive);
            writer.WriteNumber("minimumCashPrizeForFireworks", value.MinimumCashPrizeForFireworks);

            writer.WriteEndObject();
        }
    }
}
