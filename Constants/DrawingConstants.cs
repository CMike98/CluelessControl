using System.Drawing.Text;

namespace CluelessControl.Constants
{
    public class DrawingConstants
    {
        #region Center Text

        public const TextFormatFlags TEXT_CENTER_DRAWING_FORMAT_FLAGS = TextFormatFlags.NoPrefix | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.WordBreak;

        #endregion

        #region Box

        /// <summary>
        /// Outside background color of the boxes
        /// </summary>
        public static readonly Color BOX_BACKGROUND_OUT = Color.FromArgb(red: 0, green: 191, blue: 255);

        /// <summary>
        /// Inside background color of the boxes
        /// </summary>
        public static readonly Color BOX_BACKGROUND_IN = Color.Black;

        #endregion

        #region Introduce

        /// <summary>
        /// Font family used when drawing the questions
        /// </summary>
        public static readonly FontFamily INTRODUCTION_DRAWING_FONT_FAMILY = new(name: "Lato");

        /// <summary>
        /// Font size used when drawing the big questions
        /// </summary>
        public const float INTRODUCTION_DRAWING_DEFAULT_FONT_SIZE = 64.0f;

        /// <summary>
        /// Font used when drawing the questions
        /// </summary>
        public static readonly Font INTRODUCTION_DRAWING_FONT = new(INTRODUCTION_DRAWING_FONT_FAMILY, INTRODUCTION_DRAWING_DEFAULT_FONT_SIZE);

        /// <summary>
        /// Size of the introduction bar with padding
        /// </summary>
        public static readonly Size INTRODUCE_BAR_SIZE_FULL = new(width: 1400, height: 300);

        /// <summary>
        /// Size of the introduction bar padding
        /// </summary>
        public static readonly Size INTRODUCE_BAR_BORDER = new(width: 10, height: 10);

        /// <summary>
        /// Location of the introduction bar
        /// </summary>
        public static readonly Point INTRODUCE_BAR_LOCATION = new(x: 250, y: 650);

        /// <summary>
        /// Location of the inside of the introduction bar
        /// </summary>
        public static readonly Point INTRODUCE_BAR_INSIDE_LOCATION = new(INTRODUCE_BAR_LOCATION.X + INTRODUCE_BAR_BORDER.Width, INTRODUCE_BAR_LOCATION.Y + INTRODUCE_BAR_BORDER.Height);

        #endregion

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
        public static readonly Color CORRECT_ANS_COLOR = Color.Green;

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
        public static readonly Point ENVELOPE_SELECT_FIRST_LOCATION = new(x: 400, y: 80);

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
        public static readonly FontFamily QUESTION_DRAWING_FONT_FAMILY = new(name: "Lato");

        /// <summary>
        /// Font size used when drawing the big questions
        /// </summary>
        public const float QUESTION_DRAWING_DEFAULT_FONT_SIZE = 40.0f;

        /// <summary>
        /// Font used when drawing the questions
        /// </summary>
        public static readonly Font QUESTION_DRAWING_FONT = new(QUESTION_DRAWING_FONT_FAMILY, QUESTION_DRAWING_DEFAULT_FONT_SIZE);

        /// <summary>
        /// Font size used when drawing the questions and answers
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
        public static readonly Size QUESTION_BAR_SIZE_FULL = new(width: 1400, height: 300);

        /// <summary>
        /// Size of the question bar padding
        /// </summary>
        public static readonly Size QUESTION_BAR_BORDER = new(width: 10, height: 10);

        /// <summary>
        /// Location of the question bar
        /// </summary>
        public static readonly Point QUESTION_BAR_LOCATION = new(x: 250, y: 650);

        /// <summary>
        /// Location of the inside of the question bar
        /// </summary>
        public static readonly Point QUESTION_BAR_INSIDE_LOCATION = new(QUESTION_BAR_LOCATION.X + QUESTION_BAR_BORDER.Width, QUESTION_BAR_LOCATION.Y + QUESTION_BAR_BORDER.Height);

        #endregion

        #region Question Counter

        /// <summary>
        /// Question counter location
        /// </summary>
        public static readonly Point QUESTION_COUNTER_LOCATION = new(x: 250, y: 550);

        /// <summary>
        /// Question counter size (with padding)
        /// </summary>
        public static readonly Size QUESTION_COUNTER_SIZE_FULL = new(width: 400, height: 75);

        /// <summary>
        /// Question counter size padding
        /// </summary>
        public static readonly Size QUESTION_COUNTER_BORDER = new(width: 10, height: 10);

        /// <summary>
        /// Question counter font family
        /// </summary>
        public static readonly FontFamily QUESTION_COUNTER_FONT_FAMILY = new("Lato");

        /// <summary>
        /// Question counter font size
        /// </summary>
        public const float QUESTION_COUNTER_DEFAULT_FONT_SIZE = 26.0f;

        /// <summary>
        /// Question counter font
        /// </summary>
        public static readonly Font QUESTION_COUNTER_FONT = new(QUESTION_COUNTER_FONT_FAMILY, QUESTION_COUNTER_DEFAULT_FONT_SIZE);

        #endregion

        #region Trading

        /// <summary>
        /// What part of trading boxes will be the title?
        /// </summary>
        public const float TRADING_TITLE_PART_FRACTION = 0.4f;

        /// <summary>
        /// What part of trading boxes will be below the title?
        /// </summary>
        public const float TRADING_REST_PART_FRACTION = 1 - TRADING_TITLE_PART_FRACTION;


        /// <summary>
        /// Trading font family
        /// </summary>
        public static readonly FontFamily TRADING_FONT_FAMILY = new("Lato");

        /// <summary>
        /// Trading font size (big)
        /// </summary>
        public const float TRADING_BIG_FONT_SIZE = 40.0f;

        /// <summary>
        /// Trading font (big)
        /// </summary>
        public static readonly Font TRADING_BIG_FONT = new(TRADING_FONT_FAMILY, TRADING_BIG_FONT_SIZE);

        /// <summary>
        /// Trading font size (small)
        /// </summary>
        public const float TRADING_SMALL_FONT_SIZE = 32.0f;

        /// <summary>
        /// Trading font (small)
        /// </summary>
        public static readonly Font TRADING_SMALL_FONT = new(TRADING_FONT_FAMILY, TRADING_SMALL_FONT_SIZE);

        /// <summary>
        /// Location of the bottom contestant envelope (trading)
        /// </summary>
        public static readonly Point BOTTOM_TRADING_CONTESTANT_LOCATION = new(x: 230, y: 825);

        /// <summary>
        /// Location of the bottom host envelope (trading)
        /// </summary>
        public static readonly Point BOTTOM_TRADING_HOST_LOCATION = new(x: 1480, y: 825);

        /// <summary>
        /// Location of the contestant cash box
        /// </summary>
        public static readonly Point CONTESTANT_CASH_LOCATION = new(x: 500, y: 700);

        /// <summary>
        /// Size of the contestant cash box
        /// </summary>
        public static readonly Size CONTESTANT_CASH_SIZE = new(width: 400, height: 200);

        /// <summary>
        /// Padding of the contestant cash box
        /// </summary>
        public static readonly Size CONTESTANT_CASH_PADDING = new(width: 10, height: 10);

        /// <summary>
        /// Location of the host offer box
        /// </summary>
        public static readonly Point HOST_OFFER_LOCATION = new(x: 1030, y: 700);

        /// <summary>
        /// Size of the host offer box
        /// </summary>
        public static readonly Size HOST_OFFER_SIZE = new(width: 400, height: 200);

        /// <summary>
        /// Padding of the host offer box
        /// </summary>
        public static readonly Size HOST_OFFER_PADDING = new(width: 10, height: 10);

        /// <summary>
        /// Location of the word "Contestant"
        /// </summary>
        public static readonly Point CONTESTANT_WORD_LOCATION = new(x: 500, y: 575);

        /// <summary>
        /// Size of the word "Contestant"
        /// </summary>
        public static readonly Size CONTESTANT_WORD_SIZE = new(width: 400, height: 100);

        /// <summary>
        /// Padding of the box with the word "Contestant"
        /// </summary>
        public static readonly Size CONTESTANT_WORD_PADDING = new(width: 10, height: 10);

        /// <summary>
        /// Location of the word "Host"
        /// </summary>
        public static readonly Point HOST_WORD_LOCATION = new(x: 1030, y: 575);

        /// <summary>
        /// Size of the word "Host"
        /// </summary>
        public static readonly Size HOST_WORD_SIZE = new(width: 400, height: 100);

        /// <summary>
        /// Padding of the box with the word "Host"
        /// </summary>
        public static readonly Size HOST_WORD_PADDING = new(width: 10, height: 10);

        #endregion

        #region Game Over

        /// <summary>
        /// Game over font family
        /// </summary>
        public static readonly FontFamily GAME_OVER_FONT_FAMILY = new("Lato");

        /// <summary>
        /// Game over font size (big)
        /// </summary>
        public const float GAME_OVER_BIG_FONT_SIZE = 64.0f;

        /// <summary>
        /// Game over font (big)
        /// </summary>
        public static readonly Font GAME_OVER_BIG_FONT = new(GAME_OVER_FONT_FAMILY, GAME_OVER_BIG_FONT_SIZE);

        /// <summary>
        /// Game over font size (small)
        /// </summary>
        public const float GAME_OVER_SMALL_FONT_SIZE = 32.0f;

        /// <summary>
        /// Game over font (small)
        /// </summary>
        public static readonly Font GAME_OVER_SMALL_FONT = new(GAME_OVER_FONT_FAMILY, GAME_OVER_SMALL_FONT_SIZE);

        /// <summary>
        /// What part of game over box will be the title?
        /// </summary>
        public const float GAME_OVER_TITLE_PART_FRACTION = 0.4f;

        /// <summary>
        /// What part of game over will be below the title?
        /// </summary>
        public const float GAME_OVER_REST_PART_FRACTION = 1 - GAME_OVER_TITLE_PART_FRACTION;

        /// <summary>
        /// Location of the game over box
        /// </summary>
        public static readonly Point GAME_OVER_LOCATION = new(x: 250, y: 650);

        /// <summary>
        /// Size of the game over box
        /// </summary>
        public static readonly Size GAME_OVER_SIZE = new(width: 1400, height: 300);

        /// <summary>
        /// Padding of the game over box
        /// </summary>
        public static readonly Size GAME_OVER_PADDING = new(width: 10, height: 10);

        #endregion
    }
}
