using System;
using RateCalculationEngine.Model;

namespace RateCalculationEngine.RateCalculator
{
    public class FlatRate : IRateTypeStrategy
    {
        private IFlatRateCalculator _flatRateCalculator;

        public FlatRate(IFlatRateCalculator flatRateCalculator)
        {
            _flatRateCalculator = flatRateCalculator;
        }

        public RateType RateType { get { return RateType.FlatRate; } }

        public double Calculate(Patron patron)
        {
            return _flatRateCalculator
                .Calculate(patron.FlatRateType);
        }
    }
}
