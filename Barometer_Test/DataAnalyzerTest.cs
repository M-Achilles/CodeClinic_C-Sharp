using Barometer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Barometer_Test
{
    [TestClass]
    public class DataAnalyzerTest
    {
        [TestMethod]
        public void CalculateSlope_Positiv()
        {
            var x1 = new DateTime(2012, 01, 01, 00, 02, 14);
            var y1 = (float)30.5;

            var x2 = new DateTime(2012, 01, 01, 00, 08, 29);
            var y2 = (float)35.5;

            var result = DataAnalyzer.CalculateSlope((x1, y1), (x2, y2));

            Assert.AreEqual(true, result > 0);
        }

        [TestMethod]
        public void CalculateSlope_Negative()
        {
            var x1 = new DateTime(2012, 01, 01, 00, 02, 14);
            var y1 = (float)30.5;

            var x2 = new DateTime(2012, 01, 01, 00, 08, 29);
            var y2 = (float)20.5;

            var result = DataAnalyzer.CalculateSlope((x1, y1), (x2, y2));

            Assert.AreEqual(true, result < 0);
        }
    }
}
