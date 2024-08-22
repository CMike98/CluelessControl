﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl
{
    public class JsonQuestionConverter : JsonConverter<Question>
    {
        public override Question? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            // Read the question text
            string text = root.GetProperty("text").GetString() ?? throw new JsonException("No question text.");

            // Read answers
            string answer1 = root.GetProperty("answer1").GetString() ?? throw new JsonException("No answer 1 text.");
            string answer2 = root.GetProperty("answer2").GetString() ?? throw new JsonException("No answer 2 text.");
            string answer3 = root.GetProperty("answer3").GetString() ?? throw new JsonException("No answer 3 text.");
            string answer4 = root.GetProperty("answer4").GetString() ?? throw new JsonException("No answer 4 text.");

            // Read the correct answer
            int correctAnswerNumber = root.GetProperty("correctAnswerNumber").GetInt32();
            if (correctAnswerNumber < 1 || correctAnswerNumber > 4)
                throw new JsonException($"The correct answer number must be in range [1...4].");

            // Read the explanation (may be null)
            string? explanation = root.GetProperty("explanation").GetString();

            // Create the question
            return Question.Create(text, answer1, answer2, answer3, answer4, correctAnswerNumber, explanation);
        }

        public override void Write(Utf8JsonWriter writer, Question value, JsonSerializerOptions options)
        {
            // Start writing the question
            writer.WriteStartObject();
            
            // Write the question text
            writer.WriteString("text", value.Text);

            // Write the possible answers
            writer.WriteString("answer1", value.Answer1);
            writer.WriteString("answer2", value.Answer2);
            writer.WriteString("answer3", value.Answer3);
            writer.WriteString("answer4", value.Answer4);

            // Write the correct answer
            writer.WriteNumber("correctAnswerNumber", value.CorrectAnswerNumber);

            // Write the explanation
            writer.WriteString("explanation", value.Explanation);

            // End writing the question
            writer.WriteEndObject();
        }
    }
}
