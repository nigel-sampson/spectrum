﻿using System;

namespace Spectrum
{
    public static partial class Color
    {
        public class CYMK : IEquatable<CYMK>
        {
            private readonly double c;
            private readonly double y;
            private readonly double m;
            private readonly double k;

            public CYMK(double c, double y, double m, double k)
            {
                this.c = c;
                this.y = y;
                this.m = m;
                this.k = k;
            }

            public double C
            {
                get
                {
                    return c;
                }
            }

            public double Y
            {
                get
                {
                    return y;
                }
            }

            public double M
            {
                get
                {
                    return m;
                }
            }

            public double K
            {
                get
                {
                    return k;
                }
            }

            public bool Equals(CYMK other)
            {
                return C.Equals(other.C) && Y.Equals(other.Y) && M.Equals(other.M) && K.Equals(other.K);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) 
                    return false;

                if (ReferenceEquals(this, obj)) 
                    return true;

                return obj.GetType() == GetType() && Equals((CYMK)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = c.GetHashCode();
                    hashCode = (hashCode * 397) ^ y.GetHashCode();
                    hashCode = (hashCode * 397) ^ m.GetHashCode();
                    hashCode = (hashCode * 397) ^ k.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}