using System;
using System.ComponentModel;

namespace RateCalculationEngine.RateCalculator
{
    public enum RateType
    {
        FlatRate,
        [Description("Standard Rate")]
        HourlyRate
    }
}
