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

				return H.Equals(other.H) && S.Equals(other.S) && L.Equals(other.L);
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
        }
    }
}
