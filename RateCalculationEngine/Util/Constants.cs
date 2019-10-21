using System;
namespace RateCalculationEngine.Util
{
    public static class Constants
    {
        //Flat Rates
        public const string EarlyBirdRate = "FlatRate:EarlyBirdRate";
        public const string NightRate = "FlatRate:NightRate";
        public const string WeekendRate = "FlatRate:WeekendRate";

        //Standard Rates
        public const string RatePerHour = "StandardRate:RatePerHour";
        public const string RatePerDay = "StandardRate:RatePerDay";

        public const string StandardRateAppliesCondition = "StandardRateAppliesAfter";
    }
}
