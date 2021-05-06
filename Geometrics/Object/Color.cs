using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public record Color
    {
        public double Red { get; init; }
        public double Green { get; init; }
        public double Blue { get; init; }

        public Color() { }

        public Color(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Color Scale(double scalar)
        {
            return new Color
            {
                Red = this.Red * scalar,
                Green = this.Green * scalar,
                Blue = this.Blue * scalar
            };
        }

        public Color Offset(double offset)
        {
            return new Color
            {
                Red = Red + offset,
                Green = Green + offset,
                Blue = Blue + offset
            };
        }

        public static Color operator +(Color a, Color b)
        {
            return new Color
            {
                Red = a.Red + b.Red,
                Green = a.Green + b.Green,
                Blue = a.Blue + b.Blue
            };
        }

        public static Color operator *(Color a, Color b)
        {
            return new Color
            {
                Red = a.Red * b.Red,
                Green = a.Green * b.Green,
                Blue = a.Blue * b.Blue
            };
        }

        public Color Legalize()
        {
            return new Color
            {
                Red = Red > 1.0 ? 1.0 : Red,
                Green = Green > 1.0 ? 1.0 : Green,
                Blue = Blue > 1.0 ? 1.0 : Blue
            };
        }

        public static uint ToInt(double c)
        {
            uint r = (uint)(255 * c);
            
            return r > 255 ? 255 : r;
        }

        public uint ToUint()
        {
            return (uint)255 << 24 | ToInt(Red) << 16 | ToInt(Green) << 8 | ToInt(Blue);
        }
    }
}
