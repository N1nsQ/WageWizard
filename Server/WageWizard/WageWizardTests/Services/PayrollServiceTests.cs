using WageWizard.Services;

namespace WageWizardTests.Services
{
    public class PayrollServiceTests
    {
        private const decimal TyelBasic = 0.0715m;
        private const decimal TyelSenior = 0.0865m;
        internal const decimal UnemploymentInsurancePercent = 0.0059m;

        //[Theory]
        //[InlineData(2500.00, "Basic", 178.75)]   
        //[InlineData(3000.00, "Senior", 259.50)] 
        //[InlineData(4000.00, "Basic", 286.00)]   
        //[InlineData(5000.00, "Senior", 432.50)]
        //public void CalculateTyELAmount_Calculates_Correctly(decimal grossSalary, string level, decimal expected)
        //{
        //    // Arrange
        //    decimal tyelPercent = level switch
        //    {
        //        "Basic" => TyelBasic,
        //        "Senior" => TyelSenior,
        //        _ => throw new ArgumentException("Unknown level")
        //    };

        //    // Act
        //    var result = PayrollsService.CalculateTyELAmount(grossSalary, tyelPercent);

        //    // Assert
        //    Assert.Equal(expected, result);
        //}

        //[Theory]
        //[InlineData(2500.00, 14.75)]
        //[InlineData(3000.00, 17.70)]
        //public void CalculateUnemploymentInsuranceAmount_Calculates_Correctly(decimal grossSalary, decimal expected)
        //{
        //    // Act
        //    var result = PayrollsService.CalculateUnemploymentInsuranceAmount(grossSalary, UnemploymentInsurancePercent);

        //    // Assert
        //    Assert.Equal(expected, result);
        //}

        //[Theory]
        //[InlineData(2500.00, 10.0, 250.00)] 
        //[InlineData(3000.00, 15.0, 450.00)] 
        //[InlineData(4000.00, 0.0, 0.00)]
        //[InlineData(5500.55, 11.0, 605.06)]
        //[InlineData(3500.00, 9.5, 332.50)]
        //public void CalculateWithholdingTaxAmount_Calculates_Correctly(decimal grossSalary, decimal taxPercent, decimal expected)
        //{
        //    // Act
        //    var result = PayrollsService.CalculateWithholdingTaxAmount(grossSalary, taxPercent);

        //    // Assert
        //    Assert.Equal(expected, result);
        //}

        //[Theory]
        //[InlineData(3500.55, 9.5, 0.0715, 0.0059, 2897.06)]
        //[InlineData(1234.00, 10, 0.0715, 0.0059, 1015.09)]
        //[InlineData(5555.55, 25.5, 0.0715, 0.0059, 3708.88)]
        //public void CalculateNetSalaryAmount_Calculates_Correctly(
        //decimal grossSalary,
        //decimal taxPercent,
        //decimal tyelPercent,
        //decimal unemploymentInsurancePercent,
        //decimal expected)
        //{
        //    // Act
        //    var result = PayrollsService.CalculateNetSalaryAmount(
        //    grossSalary, taxPercent, tyelPercent, unemploymentInsurancePercent);

        //    // Assert
        //    Assert.Equal(expected, result);
        //}
    
    }
    
}
