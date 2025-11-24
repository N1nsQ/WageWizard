using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WageWizard.Domain.Logic;

namespace WageWizardTests.Domain
{
    public class SalaryCalculatorTests
    {
        [Theory]
        [InlineData(1000, 0.0715, 71.50)]
        [InlineData(3500, 0.0715, 250.25)]
        [InlineData(5000, 0.0715, 357.50)]
        [InlineData(10000, 0.0715, 715.00)]
        [InlineData(10000, 0.0, 0.0)]
        [InlineData(50000, 0.0, 0.0)]
        [InlineData(3000, 0.0865, 259.50)]
        [InlineData(2562, 0.0865, 221.61)]
        public void CalculateTyELAmount_ShouldReturnCorrectRoundedValues(
        decimal gross,
        decimal percent,
        decimal expected)
        {
            var result = SalaryCalculator.CalculateTyELAmount(gross, percent);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1000, 0.0059, 5.90)]
        [InlineData(3500, 0.0059, 20.65)]
        [InlineData(5000, 0.0059, 29.50)]
        [InlineData(8000, 0.0059, 47.20)]
        [InlineData(8000, 0.0, 0.0)]
        [InlineData(2000, 0.0, 0.0)]
        public void CalculateUnemploymentInsuranceAmount_ShouldReturnCorrectRoundedValues(
        decimal gross,
        decimal percent,
        decimal expected)
        {
            var result = SalaryCalculator.CalculateUnemploymentInsuranceAmount(gross, percent);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1000, 20, 200)]
        [InlineData(3500, 20, 700)]
        [InlineData(5000, 30, 1500)]
        [InlineData(8500, 17.5, 1487.50)]
        [InlineData(4364, 0, 0)]
        public void CalculateWithholdingTaxAmount_ShouldReturnCorrectValues(
        decimal gross,
        decimal taxPercent,
        decimal expected)
        {
            var result = SalaryCalculator.CalculateWithholdingTaxAmount(gross, taxPercent);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(3500, 20, 0.0715, 0.0059, 2529.10)]
        [InlineData(5000, 25, 0.0715, 0.0059, 3363.00)]
        [InlineData(6000, 30, 0.0715, 0.0059, 3735.60)]
        [InlineData(8000, 22, 0.0715, 0.0059, 5620.80)]
        [InlineData(3500, 20, 0.0865, 0.0059, 2476.60)]
        [InlineData(5000, 25, 0.0865, 0.0059, 3288.00)]
        [InlineData(6000, 30, 0.0865, 0.0059, 3645.60)]
        [InlineData(8000, 22, 0, 0.0, 6240.00)]
        [InlineData(6000, 10, 0, 0.0, 5400.00)]
        public void CalculateNetSalaryAmount_ShouldReturnCorrectValues(
        decimal gross,
        decimal taxPercent,
        decimal tyelPercent,
        decimal unempPercent,
        decimal expectedNet)
        {
            var result = SalaryCalculator.CalculateNetSalaryAmount(
                gross,
                taxPercent,
                tyelPercent,
                unempPercent);

            result.Should().Be(expectedNet);
        }
    }
}
