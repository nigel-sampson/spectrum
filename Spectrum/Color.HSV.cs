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

                return H.IsNearTo(other.H, 1) && S.IsNearTo(other.S, 3) && V.IsNearTo(other.V, 3);
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
        }
    }
}
