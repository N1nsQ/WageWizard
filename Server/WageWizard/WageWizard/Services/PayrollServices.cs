using WageWizard.DTOs;

namespace WageWizard.Services
{
    public class PayrollServices
    {
        public static decimal CalculateTyELAmount(decimal grossSalary, decimal tyelPercent)
        {
            decimal tyelAmount = grossSalary * tyelPercent; 

            return tyelAmount;
        }

        public static decimal CalculateUnemploymentInsuranceAmount(decimal grossSalary, decimal unemploymentInsurancePercent)
        {
            decimal unemploymentInsuranceAmount = grossSalary * unemploymentInsurancePercent;

            return unemploymentInsuranceAmount;
        }

        public static decimal CalculateWithholdingTaxAmount(decimal grossSalary, decimal taxPercent)
        {
            decimal withholdingTaxAmount = grossSalary * (taxPercent / 100);

            return withholdingTaxAmount;
        }

        public static decimal CalculateNetSalaryAmount(
            decimal grossSalary, 
            decimal taxPercent, 
            decimal tyelPercent, 
            decimal unemploymentInsurancePercent)
        {
            decimal withholdingTax = CalculateWithholdingTaxAmount(grossSalary, taxPercent);
            decimal tyel = CalculateTyELAmount(grossSalary, tyelPercent);
            decimal unemploymentInsurance = CalculateUnemploymentInsuranceAmount(grossSalary, unemploymentInsurancePercent);

            decimal netSalary = grossSalary - (withholdingTax + tyel + unemploymentInsurance); 

            return netSalary;


        }

        public static SalaryStatementCalculationDto CollectSalaryStatementCalculations(decimal grossSalary, decimal taxPercent, decimal tyelPercent, decimal unemploymentInsurancePercent)
        {
            var tyelAmount = CalculateTyELAmount(grossSalary, tyelPercent);
            var unemploymentInsuranceAmount = CalculateUnemploymentInsuranceAmount(grossSalary, unemploymentInsurancePercent);
            var withholdingTax = CalculateWithholdingTaxAmount(grossSalary, taxPercent);
            var netSalary = CalculateNetSalaryAmount(grossSalary,taxPercent, tyelPercent, unemploymentInsurancePercent);

            return new SalaryStatementCalculationDto
            {
                WithholdingTax = withholdingTax,
                TyELAmount = tyelAmount,
                UnemploymentInsuranceAmount = unemploymentInsuranceAmount,
                NetSalary = netSalary
            };
    
        }
    }
}
