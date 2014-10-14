using System;

namespace Spectrum
{
    public static class DoubleExtensions
    {
        public static bool IsNearTo(this double x, double y, int digits = 3)
        {
            var difference = 1.0d / Math.Pow(10, digits);

            return Math.Abs(x - y) < difference;
        }
    }
}
