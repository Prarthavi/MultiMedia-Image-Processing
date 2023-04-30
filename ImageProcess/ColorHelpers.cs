using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcess
{
    public class ColorHelpers
    {
        //
        // Name :         ColorMultiply(double val, Color c)
        // Description :  Multiplys a scalar onto an Color.
        //
        public static Color ColorMultiply(double val, Color c)
        {
            return Color.FromArgb(
                    ClampColorElem(val * c.R),
                    ClampColorElem(val * c.G),
                    ClampColorElem(val * c.B));
        }

        //
        // Name :         ClampColorElem(double val)
        // Description :  Clamps a vales to be between 0 and 255.
        //
        public static int ClampColorElem(double val)
        {
            return (int)Math.Max(Math.Min(val, 255), 0);
        }

        //
        // Name :         ColorAddColor(Color a, Color b)
        // Description :  Adds to colors together
        //
        public static Color ColorAdd(Color a, Color b)
        {
            return Color.FromArgb(
                    ClampColorElem(a.R + b.R),
                    ClampColorElem(a.G + b.G),
                    ClampColorElem(a.B + b.B));
        }

        //
        // Name :         ColorEqual(Color a, Color b)
        // Description :  Check if two colors are equal
        //
        public static bool ColorEqual(Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        // These functions are needed to simplify mins and maxes...
        public static double Min4(double a, double b, double c, double d)
        {
            double r = a;
            if (b < r)
                r = b;
            if (c < r)
                r = c;
            if (d < r)
                r = d;
            return r;
        }

        public static double Max4(double a, double b, double c, double d)
        {
            double r = a;
            if (b > r)
                r = b;
            if (c > r)
                r = c;
            if (d > r)
                r = d;
            return r;
        }

        internal static Color ColorSub(Color a, Color b)
        {
            return Color.FromArgb(
                  ClampColorElem(a.R - b.R),
                  ClampColorElem(a.G - b.G),
                  ClampColorElem(a.B - b.B));
        }

    }
}
