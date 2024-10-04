namespace CluelessControl
{
    public static class FontHelper
    {
        public static Font GetMaxFont(string s, Graphics graphics, Font font, Size maxSize, TextFormatFlags flags, bool limitScale = true)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (font is null)
                throw new ArgumentNullException(nameof(font));
            if (maxSize.IsEmpty)
                throw new ArgumentException($"The max size variable is empty!", nameof(maxSize));

            SizeF textSize = TextRenderer.MeasureText(s, font, maxSize, flags);

            float scale = Math.Min(maxSize.Height / textSize.Height, maxSize.Width / textSize.Width);

            if (limitScale && scale > 1)
                scale = 1;

            int fontSize = (int) (font.Size * scale);
            return new Font(font.FontFamily, fontSize, font.Style);
        }
    }
}
