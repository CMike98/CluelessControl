using System.Text.Json;
using System.Text.Json.Serialization;

namespace CluelessControl.Questions
{
    public class QuestionSet
    {
        public List<Question> QuestionList
        {
            get;
            private set;
        }

        [JsonIgnore]
        public int QuestionCount => QuestionList.Count;

        private QuestionSet()
        {
            QuestionList = new List<Question>();
        }

        private QuestionSet(IList<Question> questions)
        {
            QuestionList = questions.ToList();
        }

        internal static QuestionSet Create()
        {
            return new QuestionSet();
        }

        internal static QuestionSet Create(IList<Question> questions)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));
            if (questions.Any(question => question == null))
                throw new ArgumentException($"At least one question is null.", nameof(questions));

            return new QuestionSet(questions);
        }

        public void AddNewQuestion(Question newQuestion)
        {
            ArgumentNullException.ThrowIfNull(newQuestion, nameof(newQuestion));
            QuestionList.Add(newQuestion);
        }

        public void DeleteQuestionByIndex(int questionIndex)
        {
            if (questionIndex < 0 || questionIndex >= QuestionCount)
                throw new ArgumentOutOfRangeException(nameof(questionIndex), $"Question index must be between 0 and {QuestionCount - 1}.");

            QuestionList.RemoveAt(questionIndex);
        }

        public void ClearQuestionSet()
        {
            QuestionList.Clear();
        }

        public static QuestionSet LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found.", nameof(fileName));

            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<QuestionSet>(json, GameConstants.JSON_SERIALIZER_OPTIONS) ?? throw new FileFormatException("Question set loading failed.");
        }

        public void SaveToFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string json = JsonSerializer.Serialize(this, GameConstants.JSON_SERIALIZER_OPTIONS);
            File.WriteAllText(fileName, json);
        }
    }
}
