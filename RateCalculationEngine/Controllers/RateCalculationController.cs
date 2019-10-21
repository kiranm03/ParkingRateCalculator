using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RateCalculationEngine.Model;
using RateCalculationEngine.Worker;

namespace RateCalculationEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCalculationController : ControllerBase
    {
        private readonly IValidator _validator;
        private readonly ICalculationEngine _calculationEngine;
        private readonly ILogger<RateCalculationController> _logger;

        public RateCalculationController(IValidator validator, ICalculationEngine calculationEngine, ILogger<RateCalculationController> logger)
        {
            _validator = validator;
            _calculationEngine = calculationEngine;
            _logger = logger;
        }

        [HttpGet]
        public Response Calculate(DateTime entryTime, DateTime exitTime)
        {
            _logger.LogInformation("App Started");

            _validator.Validate(entryTime, exitTime);
            var response = _calculationEngine.Process(entryTime, exitTime);

            _logger.LogInformation("App Finished");

            return response;
        }
    }
}
