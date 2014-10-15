using System;

namespace Spectrum
{
    public static partial class Color
    {
        public class HSV : IEquatable<HSV>
        {
			private readonly double h;
            private readonly double s;
            private readonly double v;

            public HSV(double h, double s, double v)
            {
                this.h = h;
                this.s = s;
                this.v = v;
            }

            public double H
            {
                get
                {
                    return h;
                }
            }

            public double S
            {
                get
                {
                    return s;
                }
            }

            public double V
            {
                get
                {
                    return v;
                }
            }

            public override string ToString()
            {
                return String.Format("{0:N1}, {1:P0}, {2:P0}", H, S, V);
            }

            public bool Equals(HSV other)
            {
                if (ReferenceEquals(null, other)) 
                    return false;

                if (ReferenceEquals(this, other)) 
                    return true;

                return H.IsNearTo(other.H, 1) && S.IsNearTo(other.S) && V.IsNearTo(other.V);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) 
                    return false;

                if (ReferenceEquals(this, obj)) 
                    return true;

                return obj.GetType() == GetType() && Equals((HSV)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = H.GetHashCode();
                    hashCode = (hashCode * 397) ^ S.GetHashCode();
                    hashCode = (hashCode * 397) ^ V.GetHashCode();
                    return hashCode;
                }
            }

            public RGB ToRGB()
            {
                if (S.IsNearTo(0.0d))
                {
                    return new RGB(
                        Convert.ToByte(V * 255.0),
                        Convert.ToByte(V * 255.0),
                        Convert.ToByte(V * 255.0));
                }

                var h1 = H / 60.0d;

                if (h1.IsNearTo(6.0d))
                    h1 = 0.0d;

                var i = (int) h1;

                var v1 = V * (1 - S);
                var v2 = V * (1 - S * (h1 - i));
                var v3 = V * (1 - S * (1 - (h1 - i)));

                double r, g, b;

                if (i == 0)
                {
                    r = V;
                    g = v3;
                    b = v1;
                }
                else if (i == 1)
                {
                    r = v2;
                    g = V;
                    b = v1;
                }
                else if (i == 2)
                {
                    r = v1;
                    g = V;
                    b = v3;
                }
                else if (i == 3)
                {
                    r = v1;
                    g = v2;
                    b = V;
                }
                else if (i == 4)
                {
                    r = v3;
                    g = v1;
                    b = V;
                }
                else
                {
                    r = V;
                    g = v1;
                    b = v2;
                }

                return new RGB(
                   Convert.ToByte(r * 255.0),
                   Convert.ToByte(g * 255.0),
                   Convert.ToByte(b * 255.0));
            }
        }
    }
}
