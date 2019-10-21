using System;
using RateCalculationEngine.RateCalculator;

namespace RateCalculationEngine.Model
{
    public class Patron
    {
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public RateType RateType { get; set; }
        public FlatRateType FlatRateType { get; set; }
    }
}
