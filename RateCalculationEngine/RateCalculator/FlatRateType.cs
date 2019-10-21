using System;
using System.ComponentModel;

namespace RateCalculationEngine.RateCalculator
{
    public enum FlatRateType
    {
        [Description("Early Bird")]
        EarlyBirdRate,
        [Description("Night Rate")]
        NightRate,
        [Description("Weekend Rate")]
        WeekendRate,
        None
    }
}
