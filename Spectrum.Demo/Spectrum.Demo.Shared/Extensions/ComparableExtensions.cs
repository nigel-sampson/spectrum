using System;

namespace Spectrum.Demo.Extensions
{
    public static class ComparableExtensions
    {
        public static T Clamp<T>(this T value, T minimum, T maximum)
            where T : IComparable<T>
        {
            if (value.CompareTo(minimum) < 0)
                return minimum;

            if (value.CompareTo(maximum) > 0)
                return maximum;

            return value;
        }
    }
}
