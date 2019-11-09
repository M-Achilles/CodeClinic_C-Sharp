using System;
using System.Collections.Generic;

namespace Barometer
{
    public interface ICoordinates<T>
    {
        void CalculateCoordinates(DateTime start, DateTime end, Data<T> data);
    }
}
