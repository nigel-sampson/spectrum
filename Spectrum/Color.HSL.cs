using System;

namespace Spectrum
{
    public static partial class Color
    {
        public class HSL : IEquatable<HSL>
        {
			private readonly double h;
			private readonly double s;
			private readonly double l;

            public HSL(double h, double s, double l)
			{
				this.h = h;
				this.s = s;
				this.l = l;
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

			public double L
			{
				get
				{
					return l;
				}
			}

			public override string ToString()
			{
				return String.Format("{0:N1}, {1:P0}, {2:P0}", H, S, L);
			}

            public bool Equals(HSL other)
			{
				if (ReferenceEquals(null, other)) 
					return false;

				if (ReferenceEquals(this, other)) 
					return true;

                return H.IsNearTo(other.H, 1) && S.IsNearTo(other.S) && L.IsNearTo(other.L);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) 
					return false;

				if (ReferenceEquals(this, obj)) 
					return true;

                return obj.GetType() == GetType() && Equals((HSL)obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					var hashCode = H.GetHashCode();
					hashCode = (hashCode * 397) ^ S.GetHashCode();
					hashCode = (hashCode * 397) ^ L.GetHashCode();
					return hashCode;
				}
			}

            public RGB ToRGB()
            {
                double v;
                double r, g, b;

                r = l;   // default to gray
                g = l;
                b = l;
                v = (l <= 0.5) ? (l * (1.0 + s)) : (l + s - l * s);
                if (v > 0)
                {
                    double m;
                    double sv;
                    int sextant;
                    double fract, vsf, mid1, mid2;

                    m = l + l - v;
                    sv = (v - m) / v;
                    var hue = (h / 360.0) * 6.0;
                    sextant = (int)hue;
                    fract = hue - sextant;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    switch (sextant)
                    {
                        case 0:
                            r = v;
                            g = mid1;
                            b = m;
                            break;
                        case 1:
                            r = mid2;
                            g = v;
                            b = m;
                            break;
                        case 2:
                            r = m;
                            g = v;
                            b = mid1;
                            break;
                        case 3:
                            r = m;
                            g = mid2;
                            b = v;
                            break;
                        case 4:
                            r = mid1;
                            g = m;
                            b = v;
                            break;
                        case 5:
                            r = v;
                            g = m;
                            b = mid2;
                            break;
                    }
                }

                return new RGB(
                    Convert.ToByte(r * 255.0),
                    Convert.ToByte(g * 255.0),
                    Convert.ToByte(b * 255.0));
            }

            public HSV ToHSV()
            {
                var h1 = H;
                var v1 = ((2 * L) + (S * (1.0d - Math.Abs(2.0d * L - 1)))) / 2.0d;
                var s1 = (2 * (v1 - L)) / v1;

                return new HSV(h1, Double.IsNaN(s1) ? 0.0d : s1, Double.IsNaN(v1) ? 0.0d : v1);
            }
        }
    }
}
