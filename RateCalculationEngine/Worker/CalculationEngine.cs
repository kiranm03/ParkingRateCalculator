using System;
using Microsoft.Extensions.Logging;
using RateCalculationEngine.Extension;
using RateCalculationEngine.Model;
using RateCalculationEngine.RateCalculator;

namespace RateCalculationEngine.Worker
{
    public class CalculationEngine : ICalculationEngine
    {
        private readonly ILogger<CalculationEngine> _logger;
        private readonly IRateCalculator _rateCalculator;

        public CalculationEngine(ILogger<CalculationEngine> logger, IRateCalculator rateCalculator)
        {
            _logger = logger;
            _rateCalculator = rateCalculator;
        }

        public Response Process(DateTime entryTime, DateTime exitTime)
        {
            SetRateTypesToModel(entryTime, exitTime, out Patron patron);
            var price = _rateCalculator.Calculate(patron);
            return BuildResponse(patron, price);
            
        }

        private Response BuildResponse(Patron patron, double price)
        {
            return new Response()
            {
                RateName = GetRateName(patron),
                ParkingPrice = price
            };
        }

        private string GetRateName(Patron patron)
        {
            return patron.RateType == RateType.HourlyRate
                ? patron.RateType.GetDescription()
                : patron.FlatRateType.GetDescription();
        }

        private void SetRateTypesToModel(DateTime entryTime, DateTime exitTime, out Patron patron)
        {
            var isEarlyBirdRate = IsEarlyBirdRate(entryTime, exitTime);
            var isNightRate = IsNightRate(entryTime, exitTime);
            var isWeekendRate = IsWeekendRate(entryTime, exitTime);
            var isStandardRate = !(isEarlyBirdRate || isNightRate || isWeekendRate);

            RateType rateType = isStandardRate ? RateType.HourlyRate : RateType.FlatRate;
            FlatRateType flatRateType = isStandardRate ? FlatRateType.None
                : IdentifyFlatRateType(isEarlyBirdRate, isNightRate, isWeekendRate);

            patron = new Patron()
            {
                EntryTime = entryTime,
                ExitTime = exitTime,
                RateType = rateType,
                FlatRateType = flatRateType
            };
            
        }

        private FlatRateType IdentifyFlatRateType(bool isEarlyBirdRate, bool isNightRate, bool isWeekendRate)
        {
            var result = FlatRateType.None;

            if (isEarlyBirdRate)
                result = FlatRateType.EarlyBirdRate;

            if (isNightRate)
                result = FlatRateType.NightRate;

            if (isWeekendRate)
                result = FlatRateType.WeekendRate;

            return result;
        }

        
        private bool IsEarlyBirdRate(DateTime entryTime, DateTime exitTime)
        {
            if (entryTime.IsWeekend())
                return false;

            if (entryTime.Date != exitTime.Date)
                return false;

            return entryTime.InRange(entryTime.Date.AddHours(6), entryTime.Date.AddHours(9))
                && exitTime.InRange(exitTime.Date.AddHours(15).AddMinutes(30), exitTime.Date.AddHours(23).AddMinutes(30));
        }

        private bool IsNightRate(DateTime entryTime, DateTime exitTime)
        {
            if (entryTime.IsWeekend())
                return false;

            return entryTime.InRange(entryTime.Date.AddHours(18), entryTime.Date.AddHours(23).AddMinutes(59))
                && exitTime.InRange(entryTime.Date.AddHours(18), entryTime.Date.AddDays(1).AddHours(6));
        }

        private bool IsWeekendRate(DateTime entryTime, DateTime exitTime)
        {
            if (!entryTime.IsWeekend())
                return false;

            return entryTime.InRange(entryTime.Date.AddHours(0), entryTime.Next(DayOfWeek.Sunday).Date.AddHours(23).AddMinutes(59))
                && exitTime.InRange(entryTime.Date.AddHours(0), entryTime.Next(DayOfWeek.Sunday).Date.AddHours(23).AddMinutes(59));
        }
    }
}
