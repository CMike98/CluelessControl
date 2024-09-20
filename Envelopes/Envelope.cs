using CluelessControl.Cheques;
using System.Text;

namespace CluelessControl.Envelopes
{
    public class Envelope
    {
        /// <summary>
        /// Dictionary converting states to colors for host/contestant screen
        /// </summary>
        private static readonly Dictionary<EnvelopeState, Color> statesToColorsForScreens = new()
        {
            { EnvelopeState.NEUTRAL,          Color.White      },
            { EnvelopeState.SELECTED,         Color.White      },
            { EnvelopeState.PLAYING_FOR,      Color.Orange     },
            { EnvelopeState.WON,              Color.LightGreen },
            { EnvelopeState.DESTROYED,        Color.Gray       },
            { EnvelopeState.MARKED_FOR_TRADE, Color.Orange     },
        };

        /// <summary>
        /// Dictionary converting states to colors for TV
        /// </summary>
        private static readonly Dictionary<EnvelopeState, Color> statesToColorsForTv = new()
        {
            { EnvelopeState.NEUTRAL,          Color.White      },
            { EnvelopeState.SELECTED,         Color.Orange     },
            { EnvelopeState.PLAYING_FOR,      Color.Orange     },
            { EnvelopeState.WON,              Color.LightGreen },
            { EnvelopeState.DESTROYED,        Color.Gray       },
            { EnvelopeState.MARKED_FOR_TRADE, Color.Orange     },
        };

        /// <summary>
        /// String builder used when converting the envelope to string for the director
        /// </summary>
        private static readonly StringBuilder sb = new StringBuilder();

        /// <summary>
        /// The envelope number
        /// </summary>
        public int EnvelopeNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// The cheque inside the envelope
        /// </summary>
        public BaseCheque Cheque
        {
            get;
            private set;
        }

        /// <summary>
        /// The state of the envelope
        /// </summary>
        public EnvelopeState State
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the envelope open?
        /// </summary>
        public bool IsOpen
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor - creates a closed, neutral envelope.
        /// </summary>
        /// <param name="envelopeNumber">The envelope number</param>
        /// <param name="cheque">The cheque</param>
        private Envelope(int envelopeNumber, BaseCheque cheque)
        {
            EnvelopeNumber = envelopeNumber;
            Cheque = cheque;
            State = EnvelopeState.NEUTRAL;
            IsOpen = false;
        }

        /// <summary>
        /// Creates the envelope - closed, neutral envelope.
        /// </summary>
        /// <param name="envelopeNumber">The envelope number</param>
        /// <param name="cheque">The cheque inside the envelope</param>
        /// <returns>Created envelope</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the envelope number is not allowed</exception>
        /// <exception cref="ArgumentNullException">Thrown if the cheque is null</exception>
        internal static Envelope Create(int envelopeNumber, BaseCheque cheque)
        {
            if (envelopeNumber < GameConstants.MIN_ENVELOPE_NUMBER || envelopeNumber > GameConstants.MAX_ENVELOPE_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(envelopeNumber), $"Envelope number ({envelopeNumber}) must be in the range [{GameConstants.MIN_ENVELOPE_NUMBER}...{GameConstants.MAX_ENVELOPE_NUMBER}]");
            if (cheque == null)
                throw new ArgumentNullException(nameof(cheque));

            return new Envelope(envelopeNumber, cheque);
        }

        /// <summary>
        /// Get the envelope background color for screens
        /// </summary>
        /// <returns>The background color</returns>
        public Color GetBackgroundColorForScreens()
        {
            return statesToColorsForScreens[State];
        }

        public Color GetBackgroundColorForTv()
        {
            return statesToColorsForTv[State];
        }

        /// <summary>
        /// Gets the text with envelope number and it's value
        /// </summary>
        /// <returns>Text with envelope number and it's value, separated with a new line</returns>
        public string GetEnvelopeValueForDirector()
        {
            sb.Clear();

            if (IsOpen)
                sb.AppendLine(string.Format("{0} (OTW.)", EnvelopeNumber));
            else
                sb.AppendLine(string.Format("{0}", EnvelopeNumber));

            sb.Append(Cheque.ToValueString());
            
            return sb.ToString();
        }

        #region Mark States
        public void MarkAsNeutral()
        {
            if (State == EnvelopeState.NEUTRAL)
                return;

            if (State == EnvelopeState.DESTROYED)
                throw new InvalidOperationException("Envelope must not be destroyed.");

            State = EnvelopeState.NEUTRAL;
        }

        public void MarkAsSelected()
        {
            if (State == EnvelopeState.SELECTED)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException($"Envelope should be in a neutral state.");

            State = EnvelopeState.SELECTED;
        }

        public void MarkAsPlayedFor()
        {
            if (State == EnvelopeState.PLAYING_FOR)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException("Envelope should be in a neutral state.");

            State = EnvelopeState.PLAYING_FOR;
        }

        public void MarkForTrade()
        {
            if (State == EnvelopeState.MARKED_FOR_TRADE)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException("Envelope should be in a 'Neutral' state.");

            State = EnvelopeState.MARKED_FOR_TRADE;
        }

        public void MarkAsWon()
        {
            if (State == EnvelopeState.WON)
                return;

            if (State != EnvelopeState.PLAYING_FOR)
                throw new InvalidOperationException("Envelope should be in a 'Playing For' state.");

            State = EnvelopeState.WON;
        }

        public void MarkAsDestroyed()
        {
            if (State == EnvelopeState.DESTROYED)
                return;

            if (State != EnvelopeState.NEUTRAL && State != EnvelopeState.PLAYING_FOR)
                throw new InvalidOperationException("Envelope should be in a neutral state or in the 'Playing For' state.");

            State = EnvelopeState.DESTROYED;
        }

        #endregion
    }
}
