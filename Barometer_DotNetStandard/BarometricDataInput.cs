using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Barometer
{
    public class BarometricDataInput : DataInput
    {
        public Data<BarometricData> Data { get; set; }

        public override void ParseData()
        {
            Data = new Data<BarometricData>();
            List<DataRow<BarometricData>> rows = new List<DataRow<BarometricData>>();
            using (stringReader)
            {
                string line = stringReader.ReadLine();
                while ((line = stringReader.ReadLine()) != null)
                {
                    try
                    {
                        var row = line.Split('\t');

                        DataRow<BarometricData> dataRow = new DataRow<BarometricData>
                        {
                            DataSet = new BarometricData(),

                            TimeStamp = DateTime.ParseExact(row[0], "yyyy_MM_dd HH:mm:ss", null)
                        };
                        dataRow.DataSet.AirTemp = float.Parse(row[1], CultureInfo.InvariantCulture);
                        dataRow.DataSet.BarometricPress = float.Parse(row[2], CultureInfo.InvariantCulture);
                        dataRow.DataSet.DewPoint = float.Parse(row[3], CultureInfo.InvariantCulture);
                        dataRow.DataSet.RelativeHumidity = float.Parse(row[4], CultureInfo.InvariantCulture);
                        dataRow.DataSet.WindDir = float.Parse(row[5], CultureInfo.InvariantCulture);
                        dataRow.DataSet.WindGust = int.Parse(row[6]);
                        dataRow.DataSet.WindSpeed = float.Parse(row[7], CultureInfo.InvariantCulture);

                        rows.Add(dataRow);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            Data.DataRows = rows;
        }
    }
}
