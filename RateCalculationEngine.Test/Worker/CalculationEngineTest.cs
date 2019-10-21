using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculationEngine.Model;
using RateCalculationEngine.RateCalculator;
using RateCalculationEngine.Worker;

namespace RateCalculationEngine.Test.Worker
{
    [TestClass]
    public class CalculationEngineTest
    {
        private CalculationEngine _subject;
        private readonly Mock<ILogger<CalculationEngine>> _logger = new Mock<ILogger<CalculationEngine>>();
        private readonly Mock<IRateCalculator> _rateCalculator = new Mock<IRateCalculator>();

        private readonly DateTime friday = new DateTime(2019, 10, 18);
        private readonly DateTime saturday = new DateTime(2019, 10, 19);
        private readonly DateTime sunday = new DateTime(2019, 10, 20);

        [TestInitialize]
        public void SetUp()
        {
            _subject = new CalculationEngine(_logger.Object, _rateCalculator.Object);
        }

        [TestMethod]
        public void Process_EarlyBirdHours_ReturnsEarlyBirdPrice()
        {
            //Arrange
            var entry = friday.Date.AddHours(7);
            var exit = friday.Date.AddHours(18);
            var expectedPrice = 13;
            var expectedName = "Early Bird";
            _rateCalculator.Setup(r => r.Calculate(It.IsAny<Patron>()))
                .Returns(expectedPrice);
            
            //Act
            var response = _subject.Process(entry, exit);
            //Assert
            Assert.AreEqual(expectedPrice, response.ParkingPrice);
            Assert.AreEqual(expectedName, response.RateName);
        }

        [TestMethod]
        public void Process_NightHours_ReturnsNightPrice()
        {
            //Arrange
            var entry = friday.Date.AddHours(19);
            var exit = saturday.Date.AddHours(5);
            var expectedPrice = 6.5;
            var expectedName = "Night Rate";
            _rateCalculator.Setup(r => r.Calculate(It.IsAny<Patron>()))
                .Returns(expectedPrice);

            //Act
            var response = _subject.Process(entry, exit);
            //Assert
            Assert.AreEqual(expectedPrice, response.ParkingPrice);
            Assert.AreEqual(expectedName, response.RateName);
        }

        [TestMethod]
        public void Process_WeekendHours_ReturnsWeekendPrice()
        {
            //Arrange
            var entry = saturday.Date.AddHours(1);
            var exit = sunday.Date.AddHours(5);
            var expectedPrice = 10;
            var expectedName = "Weekend Rate";
            _rateCalculator.Setup(r => r.Calculate(It.IsAny<Patron>()))
                .Returns(expectedPrice);

            //Act
            var response = _subject.Process(entry, exit);
            //Assert
            Assert.AreEqual(expectedPrice, response.ParkingPrice);
            Assert.AreEqual(expectedName, response.RateName);
        }

        [TestMethod]
        public void Process_StandardHours_ReturnsHourlyPrice()
        {
            //Arrange
            var entry = friday.Date.AddHours(10);
            var exit = friday.Date.AddHours(12);
            var expectedPrice = 10;
            var expectedName = "Standard Rate";
            _rateCalculator.Setup(r => r.Calculate(It.IsAny<Patron>()))
                .Returns(expectedPrice);

            //Act
            var response = _subject.Process(entry, exit);
            //Assert
            Assert.AreEqual(expectedPrice, response.ParkingPrice);
            Assert.AreEqual(expectedName, response.RateName);
        }

        [TestMethod]
        public void Process_MoreThan3Hours_ReturnsDayPrice()
        {
            //Arrange
            var entry = friday.Date.AddHours(10);
            var exit = friday.Date.AddHours(15);
            var expectedPrice = 20;
            var expectedName = "Standard Rate";
            _rateCalculator.Setup(r => r.Calculate(It.IsAny<Patron>()))
                .Returns(expectedPrice);

            //Act
            var response = _subject.Process(entry, exit);
            //Assert
            Assert.AreEqual(expectedPrice, response.ParkingPrice);
            Assert.AreEqual(expectedName, response.RateName);
        }
    }
}
