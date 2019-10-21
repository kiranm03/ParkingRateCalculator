using System;
using Microsoft.Extensions.Configuration;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.RateCalculator
{
    public class NightRate : IFlatRateStrategy
    {
        private readonly IConfiguration _configuration;

        public NightRate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FlatRateType FlatRateType { get { return FlatRateType.NightRate; } }

        public double Calculate()
        {
            return _configuration.GetValue<double>(Constants.NightRate);
        }
    }
}
