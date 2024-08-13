using System.Text.Json.Serialization;

namespace CluelessControl
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

        public QuestionSet()
        {
            QuestionList = new List<Question>();
        }

        public QuestionSet(int questionSetSize)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(questionSetSize, nameof(questionSetSize));
            QuestionList = new List<Question>(questionSetSize);
        }

        [JsonConstructor]
        public QuestionSet(List<Question> questionList)
        {
            ArgumentNullException.ThrowIfNull(questionList, nameof(questionList));
            QuestionList = questionList;
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
    }
}
