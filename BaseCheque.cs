using System.Text.Json.Serialization;

namespace CluelessControl
{
    public abstract class BaseCheque
    {
        [JsonIgnore]
        public bool IsOpen
        {
            get;
            private set;
        }

        protected BaseCheque()
        {
            IsOpen = false;
        }

        public void OpenEnvelope()
        {
            IsOpen = true;
        }

        public void CloseEnvelope()
        {
            IsOpen = false;
        }

        public abstract Color GetTextColor();

        public abstract string ToValueString();
    }
}
