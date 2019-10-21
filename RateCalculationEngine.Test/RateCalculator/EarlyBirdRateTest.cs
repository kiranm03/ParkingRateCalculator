using System;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RateCalculationEngine.RateCalculator;
using RateCalculationEngine.Util;

namespace RateCalculationEngine.Test.RateCalculator
{
    [TestClass]
    public class EarlyBirdRateTest
    {
        private EarlyBirdRate _subject;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new EarlyBirdRate(_configuration.Object);
        }

        [TestMethod]
        public void Calculate_EarlyBird_ReturnEarlyBirdRate()
        {
            //Arrange
            var expected = 13;
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expected.ToString());
            _configuration.Setup(a => a.GetSection(Constants.EarlyBirdRate))
                .Returns(configurationSection.Object);
            
            //Act
            var actual = _subject.Calculate();
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
