using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculationEngine.Controllers;
using RateCalculationEngine.Model;
using RateCalculationEngine.Worker;

namespace RateCalculationEngine.Test.Controllers
{
    [TestClass]
    public class RateCalculationControllerTest
    {
        private RateCalculationController _subject;
        private readonly Mock<ILogger<RateCalculationController>> _logger = new Mock<ILogger<RateCalculationController>>();
        private readonly Mock<ICalculationEngine> _calculationEngine = new Mock<ICalculationEngine>();
        private readonly Mock<IValidator> _validator = new Mock<IValidator>();

        private readonly DateTime friday = new DateTime(2019, 10, 18);

        [TestInitialize]
        public void SetUp()
        {
            _subject = new RateCalculationController(_validator.Object, _calculationEngine.Object, _logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_ExitTimeOlderThanEntryTime_ThrowsException()
        {
            //Arrange
            var exit = friday;
            var entry = exit.AddHours(1);
            _validator.Setup(v => v.Validate(entry, exit))
                .Throws<ArgumentException>();
            //Act
            _subject.Calculate(entry, exit);
            //Assert
        }

        [TestMethod]
        public void Calculate_ValidDates_ReturnsExpectedPrice()
        {
            //Arrange
            var entry = friday;
            var exit = entry.AddHours(1);
            var expected = new Mock<Response>().Object;
            _validator.Setup(v => v.Validate(entry, exit));
            _calculationEngine.Setup(c => c.Process(entry, exit))
                .Returns(expected);
            //Act
            var actual = _subject.Calculate(entry, exit);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
