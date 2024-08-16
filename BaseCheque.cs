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

        [JsonIgnore]
        public EnvelopeState State
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

        public Color GetBackgroundColor()
        {
            return State switch
            {
                EnvelopeState.NEUTRAL => Color.White,
                EnvelopeState.PLAYING_FOR => Color.Orange,
                EnvelopeState.WON => Color.Green,
                EnvelopeState.MARKED_FOR_TRADE => Color.Orange,
                EnvelopeState.TO_BE_DESTROYED => Color.Red,
                EnvelopeState.DESTROYED => Color.Gray,
                _ => throw new InvalidOperationException($"Not recognized envelope state: '{State}'"),
            };
        }

        public abstract Color GetTextColor();

        public abstract string ToValueString();
    }
}
