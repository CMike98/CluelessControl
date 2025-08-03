using CluelessControl.Prizes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class JsonPrizeListConverter : JsonConverter<PrizeList>
    {
        public override PrizeList? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            Dictionary<string, Prize> prizes = root.GetProperty("PrizeList").EnumerateArray().Select(prizeElement =>
            {
                string key = prizeElement.GetProperty("PrizeKey").GetString() ?? throw new JsonException($"Prize key missing.");
                Prize prize = prizeElement.GetProperty("Prize").Deserialize<Prize>(options) ?? throw new JsonException($"Invalid prize JSON.");
                return (key, prize);
            }).ToDictionary();

            return PrizeList.Create(prizes);
        }

        public override void Write(Utf8JsonWriter writer, PrizeList value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("PrizeList");
            writer.WriteStartArray();

            foreach (KeyValuePair<string, Prize> prize in value.PrizeDictionary)
            {
                writer.WriteString("PrizeKey", prize.Key);

                writer.WritePropertyName("Prize");
                writer.WriteStartObject();
                JsonSerializer.Serialize(writer, prize.Value, prize.Value.GetType(), options);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
