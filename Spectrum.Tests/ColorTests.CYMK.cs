using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spectrum.Tests
{
    public static partial class ColorTests
    {
        [TestClass]
        public class CYMK
        {
            [TestMethod]
            public void EqualsWithSameColoursReturnsTrue()
            {
                var c1 = new Color.CYMK(0, 0.46, 0.12, 0.53);
                var c2 = new Color.CYMK(0, 0.46, 0.12, 0.53);

                Assert.IsTrue(c1.Equals(c2));
            }

            [TestMethod]
            public void EqualsWithDifferentColoursReturnsFalse()
            {
                var c1 = new Color.CYMK(0, 0.46, 0.12, 0.53);
                var c2 = new Color.CYMK(0.10, 0.46, 0.12, 0.53);

                Assert.IsFalse(c1.Equals(c2));
            }
        }
    }
}
