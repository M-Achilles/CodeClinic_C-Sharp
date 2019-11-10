using System;
using System.Collections.Generic;

namespace Barometer
{
    public class BarometricCoordinates : ICoordinates<BarometricData>
    {
        public List<Coordinate> Coordinates { get; private set; }

        /// <summary>
        /// Calculates the x (barometric pressure) and y (time) values for given data
        /// Stores the coordinates in field Coordinates
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="data"></param>
        public void CalculateCoordinates(DateTime start, DateTime end, Data<BarometricData> data)
        {
            Coordinates = new List<Coordinate>();
            int day = 0;
            int hour = 0;
            foreach (var (x, y) in CalculateKeyValuePair(start, end, data))
            {
                //Too many data for one day to display all on x-axis
                //So just create an coordinate object per hour
                //WinRTXamlToolkit colapses data with same x-axis value
                //That makes the chart not to 100% correct but enough for the learning purpose...
                if ((hour == 0 && day == 0) || (hour != x.Hour && day != x.Day))
                {
                    Coordinates.Add(new Coordinate()
                    {
                        X = x.ToShortDateString(),
                        Y = y
                    });
                    day = x.Day;
                    hour = x.Hour;
                }
                else if (hour != 0 && day != 0 && hour != x.Hour && day == x.Day)
                {
                    Coordinates.Add(new Coordinate()
                    {
                        X = x.ToShortTimeString(),
                        Y = y
                    });
                    hour = x.Hour;
                }
            }
        }

        private IEnumerable<(DateTime x, float y)> CalculateKeyValuePair(DateTime start, DateTime end, Data<BarometricData> data)
        {
            foreach (var dataRow in data.DataRows.FindAll(x => x.TimeStamp >= start && x.TimeStamp <= end))
            {
                yield return (dataRow.TimeStamp, dataRow.DataSet.BarometricPress);
            }
        }
    }
    public class Coordinate
    {
        public string X { get; set; }
        public float Y { get; set; }
    }

}
