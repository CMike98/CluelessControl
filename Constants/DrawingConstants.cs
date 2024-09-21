using System.Drawing.Text;

namespace CluelessControl.Constants
{
    public class DrawingConstants
    {
        #region Envelopes

        /// <summary>
        /// How many envelopes are there in one row?
        /// </summary>
        public const int ENVELOPES_IN_ONE_ROW = 5;

        /// <summary>
        /// How many full rows of envelopes there will be?
        /// </summary>
        public const int MAX_ROWS_OF_ENVELOPES = GameConstants.MAX_ENVELOPES_COUNT / ENVELOPES_IN_ONE_ROW;

        /// <summary>
        /// Padding between envelopes
        /// </summary>
        public static readonly Size ENVELOPE_PADDING = new(width: 10, height: 5);

        /// <summary>
        /// The location of the first envelope
        /// </summary>
        public static readonly Point ENVELOPE_SELECT_FIRST_LOCATION = new(x: 350, y: 80);

        /// <summary>
        /// Size of the envelope
        /// </summary>
        public static readonly Size ENVELOPE_SIZE = new(width: 220, height: 75); //new(width: 268, height: 87);

        /// <summary>
        /// Size of the envelope with the included padding
        /// </summary>
        public static readonly Size ENVELOPE_SIZE_WITH_PADDING = new(ENVELOPE_SIZE.Width + ENVELOPE_PADDING.Width, ENVELOPE_SIZE.Height + ENVELOPE_PADDING.Height);

        /// <summary>
        /// Font family used when drawing the envelopes
        /// </summary>
        public static readonly FontFamily ENVELOPE_DRAWING_FONT_FAMILY = new(name: "Gill Sans");

        /// <summary>
        /// Font size used when drawing the envelopes
        /// </summary>
        public const float ENVELOPE_DRAWING_FONT_SIZE = 22.0f; //24.0f;

        /// <summary>
        /// Font used when drawing the envelopes
        /// </summary>
        public static readonly Font ENVELOPE_DRAWING_FONT = new(ENVELOPE_DRAWING_FONT_FAMILY, ENVELOPE_DRAWING_FONT_SIZE, style: FontStyle.Bold);

        #endregion

        #region Questions

        /// <summary>
        /// Font family used when drawing the questions
        /// </summary>
        public static readonly FontFamily QUESTION_DRAWING_FONT_FAMILY = new(name: "Arial");

        /// <summary>
        /// Font size used when drawing the questions
        /// </summary>
        public const float QUESTION_DRAWING_FONT_SIZE = 18.0f;

        /// <summary>
        /// Font used when drawing the questions
        /// </summary>
        public static readonly Font QUESTION_DRAWING_FONT = new(QUESTION_DRAWING_FONT_FAMILY, QUESTION_DRAWING_FONT_SIZE);

        #endregion
    }
}
