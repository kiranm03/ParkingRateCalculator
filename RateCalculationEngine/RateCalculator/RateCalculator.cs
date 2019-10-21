using System;
using System.Linq;
using RateCalculationEngine.Model;

namespace RateCalculationEngine.RateCalculator
{
    public class RateCalculator : IRateCalculator
    {
        private readonly IRateTypeStrategy[] _rateTypeStrategies;

        public RateCalculator(IRateTypeStrategy[] rateTypeStrategies)
        {
            _rateTypeStrategies = rateTypeStrategies;
        }

        public double Calculate(Patron patron)
        {
            return _rateTypeStrategies
                .Single(r => r.RateType.Equals(patron.RateType))
                .Calculate(patron);
        }
    }
}
