namespace CluelessControl
{
    public class DrawingConstants
    {
        /// <summary>
        /// How many envelopes are there in one row?
        /// </summary>
        public const int ENVELOPES_IN_ONE_ROW = 5;

        /// <summary>
        /// How many full rows of envelopes there will be?
        /// </summary>
        public const int MAX_ROWS_OF_ENVELOPES = Constants.MAX_ENVELOPES_COUNT / ENVELOPES_IN_ONE_ROW;

        /// <summary>
        /// Padding between envelopes
        /// </summary>
        public static readonly Size ENVELOPE_PADDING = new(width: 10, height: 5);

        /// <summary>
        /// The location of the first envelope
        /// </summary>
        public static readonly Point FIRST_ENVELOPE_LOCATION = new(x: 390, y: 80);

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
        public static readonly FontFamily DRAWING_FONT_FAMILY = new(name: "Arial");

        /// <summary>
        /// Font size used when drawing the envelopes
        /// </summary>
        public const float DRAWING_FONT_SIZE = 22.0f; //24.0f;

        /// <summary>
        /// Font used when drawing the envelopes
        /// </summary>
        public static readonly Font DRAWING_FONT = new(DRAWING_FONT_FAMILY, DRAWING_FONT_SIZE);
    }
}
