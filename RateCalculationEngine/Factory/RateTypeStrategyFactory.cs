using System;
using RateCalculationEngine.RateCalculator;

namespace RateCalculationEngine.Factory
{
    public class RateTypeStrategyFactory
    {
        private readonly IRateTypeStrategy _hourlyRate;
        private readonly IRateTypeStrategy _flatRate;


        public RateTypeStrategyFactory(HourlyRate hourlyRate, FlatRate flatRate)
        {
            _hourlyRate = hourlyRate;
            _flatRate = flatRate;
        }

        public IRateTypeStrategy[] Create()
        {
            return new IRateTypeStrategy[]
            {
                _hourlyRate,
                _flatRate
            };
        }
    }
    
}
