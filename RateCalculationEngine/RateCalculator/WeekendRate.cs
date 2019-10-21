using System;
using Microsoft.Extensions.Configuration;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.RateCalculator
{
    public class WeekendRate : IFlatRateStrategy
    {
        private readonly IConfiguration _configuration;

        public WeekendRate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FlatRateType FlatRateType { get { return FlatRateType.WeekendRate; } }

        public double Calculate()
        {
            return _configuration.GetValue<double>(Constants.WeekendRate);
        }
    }
}
