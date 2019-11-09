using System;
using System.Collections.Generic;

namespace Barometer
{
    public class BarometricCoordinates : ICoordinates<BarometricData>
    {
        public List<(DateTime X, float Y)> Coordinates { get; private set; }

        /// <summary>
        /// Calculates the x (barometric pressure) and y (time) values for given data
        /// Stores the coordinates in field Coordinates
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="data"></param>
        public void CalculateCoordinates(DateTime start, DateTime end, Data<BarometricData> data)
        {
            foreach (var item in CalculateKeyValuePair(start, end, data))
            {
                Coordinates.Add(item);
            }
        }

        private IEnumerable<(DateTime x, float y)> CalculateKeyValuePair(DateTime start, DateTime end, Data<BarometricData> data)
        {
            foreach (var dataRow in data.DataRows.FindAll(x => x.TimeStamp >= start && x.TimeStamp <= end))
            {
                yield return(dataRow.TimeStamp, dataRow.DataSet.BarometricPress);
            }
        }
    }
}
