using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Converters
{
    public class JsonQuestionSetConverter : JsonConverter<QuestionSet>
    {
        public override QuestionSet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            // Read the question list (use the JsonQuestionConverter for each question)
            IList<Question> questions = root.GetProperty("questionList")
                .EnumerateArray()
                .Select(questionElement =>
                {
                    return questionElement.Deserialize<Question>(options) ?? throw new JsonException($"Invalid question JSON.");
                })
                .ToList();

            // Create the question set
            return QuestionSet.Create(questions);
        }

        public override void Write(Utf8JsonWriter writer, QuestionSet value, JsonSerializerOptions options)
        {
            // Start writing the object
            writer.WriteStartObject();

            // Start writing the questions as an array
            writer.WritePropertyName("questionList");
            writer.WriteStartArray();

            // Write each individual question
            foreach (Question question in value.QuestionList)
            {
                JsonSerializer.Serialize(writer, question, question.GetType(), options);
            }

            // Stop writing the question array
            writer.WriteEndArray();

            // Stop writing the object
            writer.WriteEndObject();
        }
    }
}
