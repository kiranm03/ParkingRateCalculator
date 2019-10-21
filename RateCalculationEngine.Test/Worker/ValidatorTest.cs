using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculationEngine.Worker;

namespace RateCalculationEngine.Test.Worker
{
    [TestClass]
    public class ValidatorTest
    {
        private Validator _subject;
        private readonly Mock<ILogger<Validator>> _logger = new Mock<ILogger<Validator>>();
        //private readonly Mock<IRateCalculator> _rateCalculator = new Mock<IRateCalculator>();

        private readonly DateTime friday = new DateTime(2019, 10, 18);

        [TestInitialize]
        public void SetUp()
        {
            _subject = new Validator(_logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_ExitTimeOlderThanEntryTime_ThrowsException()
        {
            //Arrange
            var exit = friday;
            var entry = exit.AddHours(1);
            //Act
            _subject.Validate(entry, exit);
            //Assert
        }

        [TestMethod]
        public void Validate_EntryTimeOlderThanExitTime_ShouldPass()
        {
            //Arrange
            var entry = friday;
            var exit = entry.AddHours(1);
            //Act
            _subject.Validate(entry, exit);
            //Assert
        }
    }
}
