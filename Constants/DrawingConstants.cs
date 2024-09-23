using System.Drawing.Text;

namespace CluelessControl.Constants
{
    public class DrawingConstants
    {
        #region Answer Colors
        /// <summary>
        /// Color used to mark the locked in answers
        /// </summary>
        public static readonly Color LOCK_IN_ANS_COLOR = Color.Orange;

        /// <summary>
        /// Font color used to mark the locked in answers
        /// </summary>
        public static readonly Color LOCK_IN_ANS_FONT_COLOR = Color.Black;

        /// <summary>
        /// Color used to mark the correct answers
        /// </summary>
        public static readonly Color CORRECT_ANS_COLOR = Color.LightGreen;

        /// <summary>
        /// Font color used to mark the correct answers
        /// </summary>
        public static readonly Color CORRECT_ANS_FONT_COLOR = Color.Black;
        #endregion

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
        public const float QUESTION_DRAWING_DEFAULT_FONT_SIZE = 40.0f;

        /// <summary>
        /// Font used when drawing the questions
        /// </summary>
        public static readonly Font QUESTION_DRAWING_FONT = new(QUESTION_DRAWING_FONT_FAMILY, QUESTION_DRAWING_DEFAULT_FONT_SIZE);

        /// <summary>
        /// Font size used when drawing the questions
        /// </summary>
        public const float QUESTION_ANSWER_DRAWING_DEFAULT_FONT_SIZE = 32.0f;

        /// <summary>
        /// Font used when drawing the questions
        /// </summary>
        public static readonly Font QUESTION_ANSWER_DRAWING_FONT = new(QUESTION_DRAWING_FONT_FAMILY, QUESTION_ANSWER_DRAWING_DEFAULT_FONT_SIZE);

        #endregion

        #region Question Bar

        /// <summary>
        /// Size of the question bar with padding
        /// </summary>
        public static readonly Size QUESTION_BAR_SIZE_PADDING = new(width: 1400, height: 300);

        /// <summary>
        /// Size of the question bar padding
        /// </summary>
        public static readonly Size QUESTION_BAR_PADDING = new(width: 25, height: 25);

        /// <summary>
        /// Location of the question bar
        /// </summary>
        public static readonly Point QUESTION_BAR_LOCATION = new(x: 250, y: 650);

        /// <summary>
        /// Location of the inside of the question bar
        /// </summary>
        public static readonly Point QUESTION_BAR_INSIDE_LOCATION = new(QUESTION_BAR_LOCATION.X + QUESTION_BAR_PADDING.Width, QUESTION_BAR_LOCATION.Y + QUESTION_BAR_PADDING.Height);

        /// <summary>
        /// Outside background color of the question bar
        /// </summary>
        public static readonly Color QUESTION_BAR_BACKGROUND_OUT = Color.Purple;

        /// <summary>
        /// Inside background color of the question bar
        /// </summary>
        public static readonly Color QUESTION_BAR_BACKGROUND_IN = Color.Black;

        #endregion

        #region Question Counter

        /// <summary>
        /// Question counter location
        /// </summary>
        public static readonly Point QUESTION_COUNTER_LOCATION = new(x: 250, y: 550);

        /// <summary>
        /// Question counter size (with padding)
        /// </summary>
        public static readonly Size QUESTION_COUNTER_SIZE_PADDING = new(width: 400, height: 75);

        /// <summary>
        /// Question counter size padding
        /// </summary>
        public static readonly Size QUESTION_COUNTER_PADDING = new(width: 10, height: 10);

        /// <summary>
        /// Question counter font family
        /// </summary>
        public static readonly FontFamily QUESTION_COUNTER_FONT_FAMILY = new("Arial");

        /// <summary>
        /// Question counter font size
        /// </summary>
        public const float QUESTION_COUNTER_DEFAULT_FONT_SIZE = 26.0f;

        /// <summary>
        /// Question counter font
        /// </summary>
        public static readonly Font QUESTION_COUNTER_FONT = new(QUESTION_COUNTER_FONT_FAMILY, QUESTION_COUNTER_DEFAULT_FONT_SIZE);

        /// <summary>
        /// Outside background color of the counter
        /// </summary>
        public static readonly Color QUESTION_COUNTER_BACKGROUND_OUT = Color.Purple;

        /// <summary>
        /// Inside background color of the counter
        /// </summary>
        public static readonly Color QUESTION_COUNTER_BACKGROUND_IN = Color.Black;

        #endregion
    }
}
