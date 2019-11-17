using Barometer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Barometer_Test
{
    [TestClass]
    public class BarometricCoordinatesTest
    {
        [TestMethod]
        public void CalculateCoordinates()
        {
            string testData = "date       time    	Air_Temp\tBarometric_Press\tDew_Point\tRelative_Humidity\tWind_Dir\tWind_Gust\tWind_Speed\n" +
                        "2012_01_01 00:02:14\t34.3\t30.5\t26.9\t74.2\t346.4\t11\t3.6\n" +
                        "2012_01_01 00:08:29\t34.1\t30.5\t26.5\t73.6\t349\t12\t8";

            BarometricDataInput bdi = new BarometricDataInput
            {
                InputData = testData
            };
            bdi.InitializeStringReader();
            bdi.ParseData();

            DateTime start = new DateTime(2012, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2012, 1, 1, 23, 59, 59);

            BarometricCoordinates bc = new BarometricCoordinates();
            var expectedX1 = new DateTime(2012, 1, 1, 0, 2, 14).ToShortDateString();

            var expectedY1 = (float)30.5;

            bc.CalculateCoordinates(start, end, bdi.Data);
            var result = bc.Coordinates;

            Assert.AreEqual(expectedX1, result[0].X);
            Assert.AreEqual(expectedY1, result[0].Y);
        }
    }
}