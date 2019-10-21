using System;
namespace RateCalculationEngine.Worker
{
    public interface IValidator
    {
        void Validate(DateTime entryTime, DateTime exitTime);
    }
}
