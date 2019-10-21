using System;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculationEngine.Model;
using RateCalculationEngine.RateCalculator;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.Test.RateCalculator
{
    [TestClass]
    public class HourlyRateTest
    {
        private HourlyRate _subject;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        private readonly DateTime friday = new DateTime(2019, 10, 18);

        [TestInitialize]
        public void SetUp()
        {
            _subject = new HourlyRate(_configuration.Object);
            SetConfigurationValues();
        }

        private void SetConfigurationValues()
        {
            _configuration.Setup(a => a.GetSection(Constants.RatePerDay))
                .Returns(CreateMockIConfigurationSection(20).Object);
            _configuration.Setup(a => a.GetSection(Constants.RatePerHour))
                .Returns(CreateMockIConfigurationSection(5).Object);
            _configuration.Setup(a => a.GetSection(Constants.StandardRateAppliesCondition))
                .Returns(CreateMockIConfigurationSection(3).Object);
        }

        private Mock<IConfigurationSection> CreateMockIConfigurationSection(int value)
        {
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(value.ToString());
            return configurationSection;
        }

        [TestMethod]
        public void Calculate_1hour_ReturnsHourPrice()
        {
            //Arrange
            var patron = new Patron()
            {
                EntryTime = friday.Date.AddHours(10),
                ExitTime = friday.Date.AddHours(11)
            };
            var expected = 5;
            //Act
            var actual = _subject.Calculate(patron);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_30Minutes_ReturnsHourPrice()
        {
            //Arrange
            var patron = new Patron()
            {
                EntryTime = friday.Date.AddHours(10),
                ExitTime = friday.Date.AddHours(10).AddMinutes(30)
            };
            var expected = 5;
            //Act
            var actual = _subject.Calculate(patron);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_70Minutes_Returns2HourPrice()
        {
            //Arrange
            var patron = new Patron()
            {
                EntryTime = friday.Date.AddHours(10),
                ExitTime = friday.Date.AddHours(10).AddMinutes(70)
            };
            var expected = 10;
            //Act
            var actual = _subject.Calculate(patron);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_130Minutes_Returns3HourPrice()
        {
            //Arrange
            var patron = new Patron()
            {
                EntryTime = friday.Date.AddHours(10),
                ExitTime = friday.Date.AddHours(10).AddMinutes(130)
            };
            var expected = 15;
            //Act
            var actual = _subject.Calculate(patron);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_4Hours_ReturnsDayPrice()
        {
            //Arrange
            var patron = new Patron()
            {
                EntryTime = friday.Date.AddHours(10),
                ExitTime = friday.Date.AddHours(14).AddMinutes(30)
            };
            var expected = 20;
            //Act
            var actual = _subject.Calculate(patron);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_30Hours_Returns2DaysPrice()
        {
            //Arrange
            var patron = new Patron()
            {
                EntryTime = friday.Date.AddHours(10),
                ExitTime = friday.Date.AddHours(40).AddMinutes(30)
            };
            var expected = 40;
            //Act
            var actual = _subject.Calculate(patron);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
