using System;
using System.Windows.Media;

namespace HeroSkin.Utils
{
    public static class ColorUtils
    {
        public static Color GetColorFromHSLWithAlpha(float[] hsl, byte alpha)
        {
            Color color = GetColorFromHSL(hsl);
            color.A = alpha;
            return color;
        }

        public static Color GetColorFromHSL(float[] hsl)
        {
            float C = (1 - MathF.Abs((2 * hsl[2]) - 1)) * hsl[1];
            float X = C * (1 - MathF.Abs((hsl[0] / 60 % 2) - 1));
            float m = hsl[2] - C / 2;

            float[] rgb;

            if(0 <= hsl[0] && hsl[0] < 60)
                rgb = new float[] { C, X, 0 };
            else if (60 <= hsl[0] && hsl[0] < 120)
                rgb = new float[] { X, C, 0 };
            else if (120 <= hsl[0] && hsl[0] < 180)
                rgb = new float[] { 0, C, X };
            else if (180 <= hsl[0] && hsl[0] < 240)
                rgb = new float[] { 0, X, C };
            else if (240 <= hsl[0] && hsl[0] < 300)
                rgb = new float[] { X, 0, C };
            else
                rgb = new float[] { C, 0, X };

            return Color.FromRgb(Convert.ToByte((rgb[0] + m) * 255), Convert.ToByte((rgb[1] + m) * 255), Convert.ToByte((rgb[2] + m) * 255));
        }

        public static float[] GetHSLFromColor(Color color) => GetHSLFromRGB(color.R, color.G, color.B);

        public static float[] GetHSLFromRGB(int r, int g, int b)
        {
            float[] hsl = new float[3];

            float R = r / 255;
            float G = g / 255;
            float B = b / 255;

            float CMax = MathF.Max(MathF.Max(R, G), B);
            float CMin = MathF.Min(MathF.Min(R, G), B);
            float delta = CMax - CMin;

            if (delta == 0)
                hsl[0] = 0;
            else if (CMax == R)
                hsl[0] = (G - B) / delta;
            else if (CMax == G)
                hsl[0] = 2f + ((B - R) / delta);
            else
                hsl[0] = 4f + ((R - G) / delta);
            hsl[0] *= 60;
            if (hsl[0] < 0)
                hsl[0] += 360;
            hsl[0] = MathF.Round(hsl[0]);

            hsl[2] = (CMax + CMin) / 2;

            hsl[1] = delta == 0 ? 0 : delta / (1f - MathF.Abs((2f * hsl[2]) - 1f));

            return hsl;
        }
    }
}
