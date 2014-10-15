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

            [TestMethod]
            public void ToRGBWithAccentReturnsCorrectValues()
            {
                var accent = new Color.HSL(36.6, 0.928, 0.488);
                var rgb = accent.ToRGB();

                Assert.AreEqual(new Color.RGB(240, 150, 9), rgb);
            }

            [TestMethod]
            public void ToRGBWithWhiteReturnsCorrectValues()
            {
                var accent = new Color.HSL(0, 0, 1);
                var rgb = accent.ToRGB();

                Assert.AreEqual(new Color.RGB(255, 255, 255), rgb);
            }

            [TestMethod]
            public void ToRGBWithBlackReturnsCorrectValues()
            {
                var accent = new Color.HSL(0, 0, 0);
                var rgb = accent.ToRGB();

                Assert.AreEqual(new Color.RGB(0, 0, 0), rgb);
            }

            [TestMethod]
            public void ToHSVWithAccentReturnsCorrectValues()
            {
                var accent = new Color.HSL(36.6, 0.928, 0.488);
                var hsv = accent.ToHSV();

                Assert.AreEqual(new Color.HSV(36.6, 0.963, 0.941), hsv);
            }

            [TestMethod]
            public void ToHSVWithWhiteReturnsCorrectValues()
            {
                var accent = new Color.HSL(0, 0, 1);
                var hsv = accent.ToHSV();

                Assert.AreEqual(new Color.HSV(0, 0, 1), hsv);
            }

            [TestMethod]
            public void ToHSVWithBlackReturnsCorrectValues()
            {
                var accent = new Color.HSL(0, 0, 0);
                var hsv = accent.ToHSV();

                Assert.AreEqual(new Color.HSV(0, 0, 0), hsv);
            }
        }
    }
}
