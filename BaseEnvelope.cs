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

        public string ToDisplayString()
        {
            if (IsOpen)
                return ToInternalString();
            else
                return string.Empty;
        }

        protected abstract string ToInternalString();
    }
}
