﻿using System.Text.Json.Serialization;

namespace CluelessControl.Questions
{
    public class Question
    {
        public string Text
        {
            get;
        }

        public string Answer1
        {
            get;
        }

        public string Answer2
        {
            get;
        }

        public string Answer3
        {
            get;
        }

        public string Answer4
        {
            get;
        }

        public int CorrectAnswerNumber
        {
            get;
        }

        public string Comment
        {
            get;
        }

        [JsonIgnore]
        public bool IsCommentPresent => !string.IsNullOrWhiteSpace(Comment);

        private Question(string text, string answer1, string answer2, string answer3, string answer4, int correctAnswerNumber, string comment)
        {
            Text = text.Trim();
            Answer1 = answer1.Trim();
            Answer2 = answer2.Trim();
            Answer3 = answer3.Trim();
            Answer4 = answer4.Trim();
            CorrectAnswerNumber = correctAnswerNumber;
            Comment = comment;
        }

        public static Question Create(string text, string answer1, string answer2, string answer3, string answer4, int correctAnswerNumber, string comment)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(answer1))
                throw new ArgumentNullException(nameof(answer1));
            if (string.IsNullOrWhiteSpace(answer2))
                throw new ArgumentNullException(nameof(answer2));
            if (string.IsNullOrWhiteSpace(answer3))
                throw new ArgumentNullException(nameof(answer3));
            if (string.IsNullOrWhiteSpace(answer4))
                throw new ArgumentNullException(nameof(answer4));
            if (correctAnswerNumber < 1 || correctAnswerNumber > 4)
                throw new ArgumentOutOfRangeException(nameof(correctAnswerNumber), $"Correct answer number must be between 1 and 4.");
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            return new Question(text, answer1, answer2, answer3, answer4, correctAnswerNumber, comment);
        }

        public static Question Create(string text, string answer1, string answer2, string answer3, string answer4, int correctAnswerNumber)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(answer1))
                throw new ArgumentNullException(nameof(answer1));
            if (string.IsNullOrWhiteSpace(answer2))
                throw new ArgumentNullException(nameof(answer2));
            if (string.IsNullOrWhiteSpace(answer3))
                throw new ArgumentNullException(nameof(answer3));
            if (string.IsNullOrWhiteSpace(answer4))
                throw new ArgumentNullException(nameof(answer4));
            if (correctAnswerNumber < 1 || correctAnswerNumber > 4)
                throw new ArgumentOutOfRangeException(nameof(correctAnswerNumber), $"Correct answer number must be between 1 and 4.");

            return new Question(text, answer1, answer2, answer3, answer4, correctAnswerNumber, comment: string.Empty);
        }
    }
}
