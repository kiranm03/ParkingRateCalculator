using System;
using Microsoft.Extensions.Configuration;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.RateCalculator
{
    public class EarlyBirdRate : IFlatRateStrategy
    {
        private readonly IConfiguration _configuration;

        public EarlyBirdRate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FlatRateType FlatRateType { get { return FlatRateType.EarlyBirdRate; } }

        public double Calculate()
        {
            return _configuration.GetValue<double>(Constants.EarlyBirdRate);
        }
    }
}
