using System;
using RateCalculationEngine.Model;

namespace RateCalculationEngine.RateCalculator
{
    public interface IRateTypeStrategy
    {
        RateType RateType { get; }

        double Calculate(Patron patron);
    }
}
