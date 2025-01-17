using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.EnvelopeColorStates;
using System.Text;

namespace CluelessControl.Envelopes
{
    public class Envelope
    {
        /// <summary>
        /// String builder used when converting the envelope to string for the director
        /// </summary>
        private static readonly StringBuilder sb = new StringBuilder();
        
        private ColorState _envelopeColorState;

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

            _envelopeColorState = new NeutralColorState(Cheque);
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
        /// Open the envelope
        /// </summary>
        public void Open()
        {
            IsOpen = true;
        }

        /// <summary>
        /// Close the envelope
        /// </summary>
        public void Close()
        {
            IsOpen = false;
        }

        /// <summary>
        /// Toggle Open/Close Envelope
        /// </summary>
        public void ToggleOpenClose()
        {
            IsOpen = !IsOpen;
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

        public EnvelopeColorCollection GetColorsForScreen()
        {
            return _envelopeColorState.GetColorPairingForScreen();
        }

        public EnvelopeColorCollection GetColorsForTv()
        {
            return _envelopeColorState.GetColorPairingForTv();
        }

        #region Mark States
        public void MarkAsNeutral()
        {
            if (State == EnvelopeState.NEUTRAL)
                return;

            if (State == EnvelopeState.DESTROYED)
                throw new InvalidOperationException("Envelope must not be destroyed.");

            State = EnvelopeState.NEUTRAL;

            _envelopeColorState = new NeutralColorState(Cheque);
        }

        public void MarkAsNotSelected()
        {
            if (State == EnvelopeState.NOT_SELECTED)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException("Envelope must should be in a neutral state.");

            State = EnvelopeState.NOT_SELECTED;
            _envelopeColorState = new NotSelectedColorState(Cheque);
        }

        public void MarkAsSelected()
        {
            if (State == EnvelopeState.SELECTED)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException($"Envelope should be in a neutral state.");

            State = EnvelopeState.SELECTED;

            _envelopeColorState = new SelectedColorState(Cheque);
        }

        public void MarkAsPlayingFor()
        {
            if (State == EnvelopeState.PLAYING_FOR)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException("Envelope should be in a neutral state.");

            State = EnvelopeState.PLAYING_FOR;

            _envelopeColorState = new PlayingForColorState(Cheque);
        }

        public void MarkForTrade()
        {
            if (State == EnvelopeState.MARKED_FOR_TRADE)
                return;

            if (State != EnvelopeState.NEUTRAL)
                throw new InvalidOperationException("Envelope should be in a 'Neutral' state.");

            State = EnvelopeState.MARKED_FOR_TRADE;

            _envelopeColorState = new MarkedForTradeColorState(Cheque);
        }

        public void MarkAsWon()
        {
            if (State == EnvelopeState.WON)
                return;

            if (State != EnvelopeState.PLAYING_FOR)
                throw new InvalidOperationException("Envelope should be in a 'Playing For' state.");

            State = EnvelopeState.WON;

            _envelopeColorState = new WonColorState(Cheque);
        }

        public void MarkAsForDestruction()
        {
            if (State == EnvelopeState.FOR_DESTRUCTION)
                return;

            if (State != EnvelopeState.NEUTRAL && State != EnvelopeState.PLAYING_FOR)
                throw new InvalidOperationException("Envelope should be in a neutral state or in the 'Playing For' state.");

            State = EnvelopeState.FOR_DESTRUCTION;

            _envelopeColorState = new ForDestructionColorState(Cheque);
        }

        public void MarkAsDestroyed()
        {
            if (State == EnvelopeState.DESTROYED)
                return;

            if (State != EnvelopeState.NEUTRAL && State != EnvelopeState.PLAYING_FOR && State != EnvelopeState.FOR_DESTRUCTION)
                throw new InvalidOperationException("Envelope should be in a neutral state, in the 'Playing For' state or in the 'For Destruction' state.");

            State = EnvelopeState.DESTROYED;

            _envelopeColorState = new DestroyedColorState(Cheque);
        }

        #endregion
    }
}
