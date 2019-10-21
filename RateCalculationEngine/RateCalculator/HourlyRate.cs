using System;
using Microsoft.Extensions.Configuration;
using RateCalculationEngine.Model;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.RateCalculator
{
    public class HourlyRate : IRateTypeStrategy
    {
        private readonly IConfiguration _configuration;

        public HourlyRate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RateType RateType { get { return RateType.HourlyRate; } }

        public double Calculate(Patron patron)
        {
            double price;
            var entry = patron.EntryTime;
            var exit = patron.ExitTime;
            var daysOfParking = Math.Ceiling((exit - entry).TotalDays);
            var hoursOfParking = Math.Ceiling((exit - entry).TotalHours);

            price = (hoursOfParking <= _configuration.GetValue<double>(Constants.StandardRateAppliesCondition))
                ? hoursOfParking * _configuration.GetValue<double>(Constants.RatePerHour)
                : daysOfParking * _configuration.GetValue<double>(Constants.RatePerDay);

            return price;
        }
    }
}
