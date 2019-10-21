using System;
using Microsoft.Extensions.Logging;

namespace RateCalculationEngine.Worker
{
    public class Validator : IValidator
    {
        private readonly ILogger<Validator> _logger;

        public Validator(ILogger<Validator> logger)
        {
            _logger = logger;
        }

        public void Validate(DateTime entryTime, DateTime exitTime)
        {
            if(entryTime > exitTime)
            {
                var errorMessage = $"Exit time: {exitTime} is before Enter time: {entryTime}";
                _logger.LogError(errorMessage);
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
