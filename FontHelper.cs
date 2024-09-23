using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessControl
{
    public static class FontHelper
    {
        public static Font GetMaxFont(string s, Graphics graphics, Font font, SizeF maxSize)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (font is null)
                throw new ArgumentNullException(nameof(font));
            if (maxSize.IsEmpty)
                throw new ArgumentException($"The max size variable is empty!", nameof(maxSize));

            SizeF textSize = graphics.MeasureString(s, font);

            float scale = Math.Min(maxSize.Height / textSize.Height, maxSize.Width / textSize.Width);
            if (scale > 1)
                scale = 1;

            return new Font(font.FontFamily, (int)(font.Size * scale));
        }
    }
}
