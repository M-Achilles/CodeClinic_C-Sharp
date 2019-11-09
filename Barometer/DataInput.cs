using System;
using System.IO;

namespace Barometer
{
    public abstract class DataInput<T>
    {
        string inputData;
        StringReader stringReader;

        public string InputData
        {
            get { return inputData; }
            set
            {
                if (inputData == null)
                {
                    inputData = value;
                }
                else
                {
                    inputData = string.Concat(inputData, value);
                }
            }
        }

        public void InitializeStringReader()
        {
            stringReader = new StringReader(InputData);
        }

        public abstract Data<T> ParseData(TextReader sr);
    }
}