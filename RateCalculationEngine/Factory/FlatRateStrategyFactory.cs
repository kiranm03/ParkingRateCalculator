using System;
using RateCalculationEngine.RateCalculator;

namespace RateCalculationEngine.Factory
{
    public class FlatRateStrategyFactory
    {
        private readonly IFlatRateStrategy _earlyBirdRate;
        private readonly IFlatRateStrategy _nightRate;
        private readonly IFlatRateStrategy _weekendRate;

        public FlatRateStrategyFactory(EarlyBirdRate earlyBirdRate, NightRate nightRate, WeekendRate weekendRate)
        {
            _earlyBirdRate = earlyBirdRate;
            _nightRate = nightRate;
            _weekendRate = weekendRate;
        }

        public IFlatRateStrategy[] Create()
        {
            return new IFlatRateStrategy[]
            {
                _earlyBirdRate,
                _nightRate,
                _weekendRate
            };
        }
    }
}
