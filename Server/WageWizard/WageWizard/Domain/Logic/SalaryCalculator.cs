namespace WageWizard.Domain.Logic
{
    public static class SalaryCalculator
    {
        public static decimal CalculateTyELAmount(decimal grossSalary, decimal tyelPercent)
        {
            decimal tyelAmount = Math.Round((grossSalary * tyelPercent), 2, MidpointRounding.AwayFromZero);

            return tyelAmount;

        }

        public static decimal CalculateUnemploymentInsuranceAmount(decimal grossSalary, decimal unemploymentInsurancePercent)
        {
            decimal unemploymentInsuranceAmount = Math.Round((grossSalary * unemploymentInsurancePercent), 2, MidpointRounding.AwayFromZero);

            return unemploymentInsuranceAmount;

        }

        public static decimal CalculateWithholdingTaxAmount(decimal grossSalary, decimal taxPercent)
        {

            decimal withholdingTaxAmount = Math.Round((grossSalary * (taxPercent / 100)), 2, MidpointRounding.AwayFromZero);

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

            decimal netSalary = Math.Round((grossSalary - (withholdingTax + tyel + unemploymentInsurance)), 2, MidpointRounding.AwayFromZero);

            return netSalary;

        }
    }
}
