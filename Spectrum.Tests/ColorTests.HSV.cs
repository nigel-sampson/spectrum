using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spectrum.Tests
{
    public static partial class ColorTests
    {
        [TestClass]
        public class HSV
        {
            [TestMethod]
            public void EqualsWithSameColoursReturnsTrue()
            {
                var c1 = new Color.HSV(315.3, 0.462, 0.467);
                var c2 = new Color.HSV(315.3, 0.462, 0.467);

                Assert.IsTrue(c1.Equals(c2));
            }

            [TestMethod]
            public void EqualsWithDifferentColoursReturnsFalse()
            {
                var c1 = new Color.HSV(315.3, 0.462, 0.467);
                var c2 = new Color.HSV(315.5, 0.462, 0.467);

                Assert.IsFalse(c1.Equals(c2));
            }

            [TestMethod]
            public void ToRGBWithAccentReturnsCorrectValues()
            {
                var accent = new Color.HSV(36.6, 0.963, 0.941);
                var rgb = accent.ToRGB();

                Assert.AreEqual(new Color.RGB(240, 150, 9), rgb);
            }

            [TestMethod]
            public void ToRGBWithWhiteReturnsCorrectValues()
            {
                var accent = new Color.HSV(0, 0, 1);
                var rgb = accent.ToRGB();

                Assert.AreEqual(new Color.RGB(255, 255, 255), rgb);
            }

            [TestMethod]
            public void ToRGBWithBlackReturnsCorrectValues()
            {
                var accent = new Color.HSV(0, 0, 0);
                var rgb = accent.ToRGB();

                Assert.AreEqual(new Color.RGB(0, 0, 0), rgb);
            }
        }
    }
}
