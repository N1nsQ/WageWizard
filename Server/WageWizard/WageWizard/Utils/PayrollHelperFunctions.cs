using WageWizard.Models;

namespace WageWizard.Utils
{
    public static class PayrollHelperFunctions
    {
        public static decimal GetTyELPercent(int age, int year, PayrollContext context)
        {
            if (age < 17 || age > 67)
                return 0m;

            var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year) ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");
           
            if (age >= 53 && age <= 62)
                return rates.TyEL_Senior;

            return rates.TyEL_Basic;

        }

        public static decimal GetUnemploymentInsurancePercent(int age, int year, PayrollContext context)
        {
            if (age < 18 || age >= 65)
                return 0m;

            var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year) ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

            return rates.UnemploymentInsurance;

        }
    }
}
