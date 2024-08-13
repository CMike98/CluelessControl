using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl
{
    public class JsonQuestionConverter : JsonConverter<Question>
    {
        public override Question? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            string text = root.GetProperty("text").GetString() ?? throw new JsonException("No question text.");

            string[] optionsArray = root.GetProperty("answers")
                .EnumerateArray()
                .Select(answerElement =>
                {
                    string? answerText = answerElement.GetString();
                    if (string.IsNullOrWhiteSpace(answerText))
                        throw new JsonException("At least answer is null or white space.");
                    return answerText;
                })
                .ToArray();

            if (optionsArray.Length != Constants.ANSWERS_PER_QUESTION)
                throw new JsonException($"There must be exactly {Constants.ANSWERS_PER_QUESTION} answers.");

            int correctAnswerIndex = root.GetProperty("correctAnswerIndex").GetInt32();
            if (correctAnswerIndex < 0 || correctAnswerIndex >= Constants.ANSWERS_PER_QUESTION)
                throw new JsonException($"The correct answer index must be between 0 and {Constants.ANSWERS_PER_QUESTION - 1}.");

            return Question.Create(text, optionsArray, correctAnswerIndex);
        }

        public override void Write(Utf8JsonWriter writer, Question value, JsonSerializerOptions options)
        {
            // Start writing the question
            writer.WriteStartObject();
            
            // Write the question text
            writer.WriteString("text", value.Text);

            // Write the possible answers
            writer.WritePropertyName("answers");
            writer.WriteStartArray();
            foreach (string answer in value.Answers)
            {
                writer.WriteStringValue(answer);
            }
            writer.WriteEndArray();

            // Write the correct answer
            writer.WriteNumber("correctAnswerIndex", value.CorrectAnswerIndex);

            // End writing the question
            writer.WriteEndObject();
        }
    }
}
