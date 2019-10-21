using System;
using RateCalculationEngine.Model;

namespace RateCalculationEngine.RateCalculator
{
    public interface IRateCalculator
    {
        double Calculate(Patron patron);
    }
}
