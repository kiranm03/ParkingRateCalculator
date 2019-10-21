using System;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculationEngine.RateCalculator;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.Test.RateCalculator
{
    [TestClass]
    public class NightRateTest
    {
        private NightRate _subject;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new NightRate(_configuration.Object);
        }

        [TestMethod]
        public void Calculate_NightRateHours_ReturnNightRate()
        {
            //Arrange
            var expected = 6.5;
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expected.ToString());
            _configuration.Setup(a => a.GetSection(Constants.NightRate))
                .Returns(configurationSection.Object);
            
            //Act
            var actual = _subject.Calculate();
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
