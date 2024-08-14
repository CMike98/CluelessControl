﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl
{
    public class JsonQuestionSetConverter : JsonConverter<QuestionSet>
    {
        public override QuestionSet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            IList<Question> questions = root.GetProperty("questionList")
                .EnumerateArray()
                .Select(questionElement =>
                {
                    return JsonSerializer.Deserialize<Question>(questionElement, options) ?? throw new JsonException($"Invalid question JSON.");
                })
                .ToList();

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
