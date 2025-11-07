using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard.Models;
using WageWizard.Utils;

namespace WageWizardTests.Utils
{
    public class PayrollHelperFunctionTests
    {
        private PayrollContext CreateContextWithRates(int year)
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PayrollContext(options);
            context.PayrollRates.Add(new PayrollRates
            {
                Year = year,
                TyEL_Basic = 0.0715m,
                TyEL_Senior = 0.0865m,
                UnemploymentInsurance = 0.0059m
            });

            context.SaveChanges();
            return context;
        }

        [Theory]
        [InlineData(16)] 
        [InlineData(68)]
        public void CalculateTyEL_WhenAgeOutOfRange_ReturnsZero(int age)
        {
            using var context = CreateContextWithRates(DateTime.Now.Year);
            var result = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, context);
            Assert.Equal(0m, result);
        }

        [Theory]
        [InlineData(17)]  
        [InlineData(18)]
        [InlineData(30)]  
        [InlineData(52)]  
        public void CalculateTyEL_WhenAgeBetween17And67_ReturnsBasicRate(int age)
        {
            using var context = CreateContextWithRates(DateTime.Now.Year);
            var result = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, context);
            var expected = (age < 17 || age > 67) ? 0m : 0.0715m;
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(53)]  
        [InlineData(55)]  
        [InlineData(62)]  
        public void CalculateTyEL_WhenAgeBetween53And62_ReturnsSeniorRate(int age)
        {
            using var context = CreateContextWithRates(DateTime.Now.Year);
            var result = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, context);
            Assert.Equal(0.0865m, result);
        }

        [Fact]
        public void CalculateTyEL_WhenRatesMissing_ThrowsKeyNotFoundException()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);

            var ex = Assert.Throws<KeyNotFoundException>(() =>
                PayrollHelperFunctions.CalculateTyEL(30, 2024, context));

            Assert.Contains("TyEL rates not found for year", ex.Message);
        }

        [Theory]
        [InlineData(17)]
        [InlineData(65)]
        [InlineData(80)]
        public void CalculateUnemploymentInsurance_WhenAgeOutOfRange_ReturnsZero(int age)
        {
            using var context = CreateContextWithRates(DateTime.Now.Year);
            var result = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, context);
            Assert.Equal(0m, result);
        }

        [Theory]
        [InlineData(18)]
        [InlineData(25)]
        [InlineData(40)]
        [InlineData(64)]
        public void CalculateUnemploymentInsurance_WhenAgeBetween18And64_ReturnsRate(int age)
        {
            // Arrange
            using var context = CreateContextWithRates(DateTime.Now.Year);

            // Act
            var result = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, context);

            // Assert
            Assert.Equal(0.0059m, result);
        }

        [Fact]
        public void CalculateUnemploymentInsurance_WhenRatesMissing_ThrowsKeyNotFoundException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() =>
                PayrollHelperFunctions.CalculateUnemploymentInsurace(40, 2024, context));

            Assert.Contains("TyEL rates not found for year", ex.Message);
        }

    }
}
