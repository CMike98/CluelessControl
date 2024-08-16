using System.Text.Json.Serialization;

namespace CluelessControl
{
    public abstract class BaseEnvelope
    {
        [JsonIgnore]
        public bool IsOpen
        {
            get;
            private set;
        }

        protected BaseEnvelope()
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

        public Color GetBackgroundColor()
        {
            return Color.White;
        }

        public abstract Color GetTextColor();

        public abstract string ToValueString();
    }
}
