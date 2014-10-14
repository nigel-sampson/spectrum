using System;

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
        }
    }
}
