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

        public string ToContestantString()
        {
            if (IsOpen)
                return ToHostString();
            else
                return string.Empty;
        }

        public abstract string ToHostString();
    }
}
