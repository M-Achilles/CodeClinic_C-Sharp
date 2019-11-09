using System;
using System.Collections.Generic;

namespace Barometer
{
    public class Data<T>
    {
        public Data()
        {
            DataRows = new List<DataRow<T>>();
        }
        public List<DataRow<T>> DataRows { get; set; }
    }

    public class DataRow<T>
    {
        public DateTime TimeStamp { get; set; }
        public T DataSet { get; set; }
    }
}