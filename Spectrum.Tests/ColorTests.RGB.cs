using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spectrum.Tests
{
    public static partial class ColorTests
    {
        [TestClass]
        public class RGB
        {
            [TestMethod]
            public void EqualsWithSameColoursReturnsTrue()
            {
                var c1 = new Color.RGB(240, 150, 9);
                var c2 = new Color.RGB(240, 150, 9);

                Assert.IsTrue(c1.Equals(c2));
            }

            [TestMethod]
            public void EqualsWithDifferentColoursReturnsFalse()
            {
                var c1 = new Color.RGB(240, 150, 9);
                var c2 = new Color.RGB(240, 150, 10);

                Assert.IsFalse(c1.Equals(c2));
            }

            [TestMethod]
            public void ToHexStringWithAccentFormatsString()
            {
                var accent = new Color.RGB(240, 150, 9);

                Assert.AreEqual("#F09609", accent.ToHexString());
            }

            [TestMethod]
            public void ToHexStringWithWhiteFormatsString()
            {
                var accent = new Color.RGB(255, 255, 255);

                Assert.AreEqual("#FFFFFF", accent.ToHexString());
            }

            [TestMethod]
            public void ToPercentageWithWhiteReturnsCorrectPercentages()
            {
                var white = new Color.RGB(255, 255, 255);
                var percentages = white.ToPercentage();

                Assert.AreEqual(1.0, percentages[0]);
                Assert.AreEqual(1.0, percentages[1]);
                Assert.AreEqual(1.0, percentages[2]);
            }

            [TestMethod]
            public void ToPercentageWithBlackReturnsCorrectPercentages()
            {
                var black = new Color.RGB(0, 0, 0);
                var percentages = black.ToPercentage();

                Assert.AreEqual(0, percentages[0]);
                Assert.AreEqual(0, percentages[1]);
                Assert.AreEqual(0, percentages[2]);
            }

            [TestMethod]
            public void ToPercentageWithAccentReturnsCorrectPercentages()
            {
                var accent = new Color.RGB(240, 150, 9);
                var percentages = accent.ToPercentage();

                Assert.IsTrue(Math.Abs(0.941 - percentages[0]) < 0.001);
                Assert.IsTrue(Math.Abs(0.588 - percentages[1]) < 0.001);
                Assert.IsTrue(Math.Abs(0.035 - percentages[2]) < 0.001);
            }

            [TestMethod]
            public void ToHSVWithAccentReturnsCorrectValues()
            {
                var accent = new Color.RGB(240, 150, 9);
                var hsv = accent.ToHSV();

                Assert.AreEqual(new Color.HSV(36.6, 0.963, 0.941), hsv);
            }

            [TestMethod]
            public void ToHSVWithBlackReturnsCorrectValues()
            {
                var accent = new Color.RGB(0, 0, 0);
                var hsv = accent.ToHSV();

                Assert.AreEqual(new Color.HSV(0, 0, 0), hsv);
            }

            [TestMethod]
            public void ToHSVWithWhiteReturnsCorrectValues()
            {
                var accent = new Color.RGB(255, 255, 255);
                var hsv = accent.ToHSV();

                Assert.AreEqual(new Color.HSV(0, 0, 1), hsv);
            }

            [TestMethod]
            public void ToHSLWithAccentReturnsCorrectValues()
            {
                var accent = new Color.RGB(240, 150, 9);
                var hsl = accent.ToHSL();

                Assert.AreEqual(new Color.HSL(36.6, 0.928, 0.488), hsl);
            }

            [TestMethod]
            public void ToHSLWithBlackReturnsCorrectValues()
            {
                var accent = new Color.RGB(0, 0, 0);
                var hsv = accent.ToHSL();

                Assert.AreEqual(new Color.HSL(0, 0, 0), hsv);
            }

            [TestMethod]
            public void ToHSLWithWhiteReturnsCorrectValues()
            {
                var accent = new Color.RGB(255, 255, 255);
                var hsv = accent.ToHSL();

                Assert.AreEqual(new Color.HSL(0, 0, 1), hsv);
            }
        }
    }
}
