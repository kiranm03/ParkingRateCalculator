using System;
using RateCalculationEngine.Model;

namespace RateCalculationEngine.Worker
{
    public interface ICalculationEngine
    {
        Response Process(DateTime entryTime, DateTime exitTime);
    }
}
