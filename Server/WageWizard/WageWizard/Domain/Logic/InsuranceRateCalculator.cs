using WageWizard.Domain.Entities;

namespace WageWizard.Domain.Logic
{
    public static class InsuranceRateCalculator
    {
        public static decimal GetTyELPercent(int age, PayrollRates rates)
        {
            if (age < 17 || age > 67)
                return 0m;

            if (age >= 53 && age <= 62)
                return rates.TyEL_Senior;

            return rates.TyEL_Basic;

        }

        public static decimal GetUnemploymentInsurancePercent(int age, PayrollRates rates)
        {
            if (age < 18 || age >= 65)
                return 0m;

            return rates.UnemploymentInsurance;

        }
    }
}
