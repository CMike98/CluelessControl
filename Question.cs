using System.Text.Json.Serialization;

namespace CluelessControl
{
    public class Question
    {
        public string Text
        {
            get;
        }

        public string[] Answers
        {
            get;
        }

        public int CorrectAnswerIndex
        {
            get;
        }

        public string? Explanation
        {
            get;
        }

        [JsonIgnore]
        public bool IsExplanationPresent => !string.IsNullOrWhiteSpace(Explanation);

        private Question(string text, string[] answers, int correctAnswerIndex, string? explanation)
        {
            Text = text.Trim();
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
        }

        public static Question Create(string text, string[] answers, int correctAnswerIndex, string? explanation)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (answers == null)
                throw new ArgumentNullException(nameof(answers));
            if (answers.Length != Constants.ANSWERS_PER_QUESTION)
                throw new ArgumentException($"There must be exactly {Constants.ANSWERS_PER_QUESTION} answers.", nameof(answers));
            if (answers.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("At least one answer is null or white space string.", nameof(answers));
            if (correctAnswerIndex < 0 || correctAnswerIndex >= answers.Length)
                throw new ArgumentOutOfRangeException(nameof(correctAnswerIndex), $"Correct answer index must be between 0 and {Constants.ANSWERS_PER_QUESTION - 1}.");

            return new Question(text, answers, correctAnswerIndex, explanation);
        }

        public static Question Create(string text, string[] answers, int correctAnswerIndex)
        {
            return Create(text, answers, correctAnswerIndex, null);
        }
    }
}
