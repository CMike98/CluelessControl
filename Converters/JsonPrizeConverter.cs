using CluelessControl.Prizes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class JsonPrizeConverter : JsonConverter<Prize>
    {
        public override Prize? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            string prizeName = root.GetProperty("PrizeName").GetString() ?? throw new JsonException("No prize code.");
            decimal roundingUnit = root.GetProperty("RoundingUnit").GetDecimal();
            RoundingMethod roundingMethod = (RoundingMethod) root.GetProperty("RoundingMethod").GetInt32();

            return Prize.CreatePrize(prizeName, roundingUnit, roundingMethod);
        }

        public override void Write(Utf8JsonWriter writer, Prize value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("PrizeName", value.PrizeName);
            writer.WriteNumber("RoundingUnit", value.RoundingUnit);
            writer.WriteNumber("RoundingMethod", (int)value.RoundingMethod);

            writer.WriteEndObject();
        }
    }
}
