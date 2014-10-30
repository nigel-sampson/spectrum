using System;
using System.Globalization;
using System.Linq;

namespace Spectrum
{
    public static partial class Color
    {
        public class RGB : IEquatable<RGB>
        {
            private readonly byte r;
            private readonly byte g;
            private readonly byte b;

            public RGB(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public RGB(string hex)
            {
                if (hex.StartsWith("#"))
                    hex = hex.Substring(1);

                if (hex.Length == 6)
                {
                    r = Byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                    g = Byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                    b = Byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                }
                else if (hex.Length == 8)
                {
                    r = Byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                    g = Byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                    b = Byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
                }
            }

            public byte R
            {
                get
                {
                    return r;
                }
            }

            public byte G
            {
                get
                {
                    return g;
                }
            }

            public byte B
            {
                get
                {
                    return b;
                }
            }

            public override string ToString()
            {
                return String.Format("{0}, {1}, {2}", R, G, B);
            }

            public string ToHexString()
            {
                return String.Format("#{0:X2}{1:X2}{2:X2}", R, G, B);
            }

            public bool Equals(RGB other)
            {
                if (ReferenceEquals(null, other))
                    return false;

                if (ReferenceEquals(this, other))
                    return true;

                return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;

                if (ReferenceEquals(this, obj))
                    return true;

                return obj.GetType() == GetType() && Equals((RGB)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = R.GetHashCode();
                    hashCode = (hashCode * 397) ^ G.GetHashCode();
                    hashCode = (hashCode * 397) ^ B.GetHashCode();
                    return hashCode;
                }
            }

            public double[] ToPercentage()
            {
                return new[] { R/255.0d, G/255.0d, B/255.0d };
            }

            public HSV ToHSV()
            {
                var percentage = ToPercentage();

                var min = percentage.Min();
                var max = percentage.Max();

                var delta = max - min;

                var v = max;
                double s;
                double h;

                if (max > 0.0d)
                {
                    s = delta / max;
                }
                else
                {
                    s = 0.0d;
                }

                if (Math.Abs(percentage[0] - percentage.Max()) < 0.01)
                {
                    h = (percentage[1] - percentage[2]) / delta;
                }
                else if (Math.Abs(percentage[1] - percentage.Max()) < 0.01)
                {
                    h = 2 + (percentage[2] - percentage[1]) / delta;
                }
                else
                {
                    h = 4 + (percentage[0] - percentage[1]) / delta;
                }

                h *= 60;

                if (h < 0.0d)
                    h += 360.0d;

                h = Double.IsNaN(h) ? 0.0d : h;
                s = Double.IsNaN(s) ? 0.0d : s;

                return new HSV(h, s, v);
            }

            public HSL ToHSL()
            {
                var percentage = ToPercentage();

                var min = percentage.Min();
                var max = percentage.Max();

                var delta = max - min;

                var l = (max + min) / 2.0d;
                double s;
                double h;

                if (max > 0.0d)
                {
                    if (l < 0.5d)
                        s = delta / (max + min);
                    else
                        s = delta / (2 - max - min);
                }
                else
                {
                    s = 0;
                }

                if (Math.Abs(percentage[0] - percentage.Max()) < 0.01)
                {
                    h = (percentage[1] - percentage[2]) / delta;
                }
                else if (Math.Abs(percentage[1] - percentage.Max()) < 0.01)
                {
                    h = 2 + (percentage[2] - percentage[0]) / delta;
                }
                else
                {
                    h = 4 + (percentage[0] - percentage[1]) / delta;
                }

                h *= 60;

                if (h < 0.0d)
                    h += 360.0d;

                h = Double.IsNaN(h) ? 0.0d : h;
                s = Double.IsNaN(s) ? 0.0d : s;

                return new HSL(h, s, l);
            }
        }
    }
}
