using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Logic;
using FluentAssertions;

namespace WageWizardTests.Domain
{
    public class InsuranceRateCalculatorTests
    {
        private readonly PayrollRates _rates = new PayrollRates
        {
            TyEL_Basic = 0.0715m,
            TyEL_Senior = 0.0865m,
            UnemploymentInsurance = 0.0059m
        };

        [Theory]
        [InlineData(16)]
        [InlineData(68)]
        [InlineData(70)]
        public void GetTyELPercent_ReturnsZero_WhenUnder17OrOver67(int age)
        {
            var result = InsuranceRateCalculator.GetTyELPercent(age, _rates);

            result.Should().Be(0m);
        }

        [Theory]
        [InlineData(53)]
        [InlineData(60)]
        [InlineData(62)]
        public void GetTyELPercent_ReturnsSeniorRate_WhenAgeBetween53And62(int age)
        {
            var result = InsuranceRateCalculator.GetTyELPercent(age, _rates);

            result.Should().Be(_rates.TyEL_Senior);
        }

        [Theory]
        [InlineData(17)]
        [InlineData(30)]
        [InlineData(50)]
        [InlineData(52)]
        public void GetTyELPercent_ReturnsBasicRate(int age)
        {
            var result = InsuranceRateCalculator.GetTyELPercent(age, _rates);

            result.Should().Be(_rates.TyEL_Basic);
        }

        [Theory]
        [InlineData(15)]
        [InlineData(17)]
        [InlineData(65)]
        [InlineData(80)]
        public void GetUnemploymentInsurancePercent_ReturnsZero_WhenUnder18Or65Plus(int age)
        {
            var result = InsuranceRateCalculator.GetUnemploymentInsurancePercent(age, _rates);

            result.Should().Be(0m);
        }

        [Theory]
        [InlineData(18)]
        [InlineData(30)]
        [InlineData(64)]
        public void GetUnemploymentInsurancePercent_ReturnsRate_WhenAgeBetween18And64(int age)
        {
            var result = InsuranceRateCalculator.GetUnemploymentInsurancePercent(age, _rates);

            result.Should().Be(_rates.UnemploymentInsurance);
        }
    }

}
