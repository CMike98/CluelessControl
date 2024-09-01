﻿namespace CluelessControl
{
    public class Envelope
    {
        /// <summary>
        /// Dictionary converting states to colors
        /// </summary>
        private static readonly Dictionary<EnvelopeState, Color> statesToColors = new()
        {
            { EnvelopeState.NEUTRAL,          Color.White },
            { EnvelopeState.PLAYING_FOR,      Color.Orange },
            { EnvelopeState.WON,              Color.Green },
            { EnvelopeState.MARKED_FOR_TRADE, Color.Orange },
            { EnvelopeState.TO_BE_DESTROYED,  Color.Red },
            { EnvelopeState.DESTROYED,        Color.Gray }
        };

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
            set;
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
            if (envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(envelopeNumber), $"Envelope number ({envelopeNumber}) must be in the range [{Constants.MIN_ENVELOPE_NUMBER}...{Constants.MAX_ENVELOPE_NUMBER}]");
            if (cheque == null)
                throw new ArgumentNullException(nameof(cheque));

            return new Envelope(envelopeNumber, cheque);
        }

        /// <summary>
        /// Get the envelope background color
        /// </summary>
        /// <returns>The background color</returns>
        /// <exception cref="InvalidOperationException">Thrown if not recognized envelope state</exception>
        public Color GetBackgroundColor()
        {
            return statesToColors[State];
        }
    }
}
