using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard.Models;
using WageWizard.Services;

namespace WageWizardTests.Services
{
    public class PayrollServiceTests
    {
        private readonly PayrollServices _payrollServices;

        public PayrollServiceTests()
        {
            _payrollServices = new PayrollServices();
        }

        private const decimal TyelBasic = 0.0715m;
        private const decimal TyelSenior = 0.0865m;
        internal const decimal UnemploymentInsurancePercent = 0.0059m;

        [Theory]
        [InlineData(2500.00, "Basic", 178.75)]   
        [InlineData(3000.00, "Senior", 259.50)] 
        [InlineData(4000.00, "Basic", 286.00)]   
        [InlineData(5000.00, "Senior", 432.50)]
        public void CalculateTyELAmount_Calculates_Correctly(decimal grossSalary, string level, decimal expected)
        {
            // Arrange
            decimal tyelPercent = level switch
            {
                "Basic" => TyelBasic,
                "Senior" => TyelSenior,
                _ => throw new ArgumentException("Unknown level")
            };

            // Act
            var result = PayrollServices.CalculateTyELAmount(grossSalary, tyelPercent);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2500.00, 14.75)]
        [InlineData(3000.00, 17.70)]
        [InlineData(3458.44, 20.4022)]     
        public void CalculateUnemploymentInsuranceAmount_Calculates_Correctly(decimal grossSalary, decimal expected)
        {
            // Act
            var result = PayrollServices.CalculateUnemploymentInsuranceAmount(grossSalary, UnemploymentInsurancePercent);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2500.00, 10.0, 250.00)] 
        [InlineData(3000.00, 15.0, 450.00)] 
        [InlineData(4000.00, 0.0, 0.00)]
        public void CalculateWithholdingTaxAmount_Calculates_Correctly(decimal grossSalary, decimal taxPercent, decimal expected)
        {
            // Act
            var result = PayrollServices.CalculateWithholdingTaxAmount(grossSalary, taxPercent);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
