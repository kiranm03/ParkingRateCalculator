using System;
namespace RateCalculationEngine.RateCalculator
{
    public interface IFlatRateStrategy
    {
        FlatRateType FlatRateType { get; }

        double Calculate();
    }
}
