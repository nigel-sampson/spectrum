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
        }
    }
}
