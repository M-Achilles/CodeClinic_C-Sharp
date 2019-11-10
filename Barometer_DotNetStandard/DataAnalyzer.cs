using System;

namespace Barometer
{
    public static class DataAnalyzer
    {
        public static float CalculateSlope((DateTime x, float y) point1, (DateTime x, float y) point2)
        {
            float slope = (point2.y - point1.y) / (point2.x - point1.x).Seconds;

            return slope;
        }
        public static float CalculateSlope((string x, float y) point1, (string x, float y) point2)
        {
            float slope = (point2.y - point1.y) / (Convert.ToDateTime(point2.x) - Convert.ToDateTime(point1.x)).Seconds;

            return slope;
        }
    }
}
