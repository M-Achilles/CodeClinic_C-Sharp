using Barometer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Barometer_Test
{
    [TestClass]
    public class BarometricDataInputTest
    {
        [TestMethod]
        public void ParseData()
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

            var result = bdi.Data.DataRows[0].DataSet.WindSpeed;

            Assert.AreEqual(3.6, result, 0.1);
        }

        [TestMethod]
        public void InputData()
        {
            BarometricDataInput bdi = new BarometricDataInput
            {
                InputData = "TestString1 "
            };

            Assert.AreEqual(bdi.InputData, "TestString1 ");

            bdi.InputData = "TestString2";

            Assert.AreEqual(bdi.InputData, "TestString1 TestString2");
        }
    }
}
