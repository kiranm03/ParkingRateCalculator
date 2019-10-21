using System;
namespace RateCalculationEngine.RateCalculator
{
    public interface IFlatRateCalculator
    {
        double Calculate(FlatRateType flatRateType);
    }
}
