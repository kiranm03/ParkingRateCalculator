using System.Linq;
namespace RateCalculationEngine.RateCalculator
{
    public class FlatRateCalculator : IFlatRateCalculator
    {
        private readonly IFlatRateStrategy[] _flatRateStrategies;

        public FlatRateCalculator(IFlatRateStrategy[] flatRateStrategies)
        {
            _flatRateStrategies = flatRateStrategies;
        }

        public double Calculate(FlatRateType flatRateType)
        {
            return _flatRateStrategies
                .Single(r => r.FlatRateType.Equals(flatRateType))
                .Calculate();
        }
    }
}
