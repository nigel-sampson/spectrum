using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spectrum.Tests
{
    public static partial class ColorTests
    {
        [TestClass]
        public class HSL
        {
            [TestMethod]
            public void EqualsWithSameColoursReturnsTrue()
            {
                var c1 = new Color.HSL(315.3, 0.462, 0.467);
                var c2 = new Color.HSL(315.3, 0.462, 0.467);

                Assert.IsTrue(c1.Equals(c2));
            }

            [TestMethod]
            public void EqualsWithDifferentColoursReturnsFalse()
            {
                var c1 = new Color.HSL(315.3, 0.462, 0.467);
                var c2 = new Color.HSL(315.5, 0.462, 0.467);

                Assert.IsFalse(c1.Equals(c2));
            }
        }
    }
}
