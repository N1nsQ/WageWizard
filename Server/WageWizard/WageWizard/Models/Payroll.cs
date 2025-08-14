using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WageWizard.Models
{
    public class Payroll
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } // eg. "May 2025"
        public DateTime? SalaryDate { get; set; } // eg. 15 May 2025
        public DateTime? PayPeriod { get; set; } // eg. 1.5.2025 - 31.5.2025

        // Salary & Benefits
        public decimal? BaseSalary { get; set; }
        public decimal? MealBenefit { get; set; }
        public decimal? PhoneBenefit { get; set; }
        public decimal? OvertimePay { get; set; }
        public decimal? GrossSalary { get; set; } // Kaikki palkat laskettuna yhteen, bruttopalkka
        public decimal? NetSalary { get; set; } // Lopullinen maksettava summa työntekijälle

        // Deductions 
        public decimal? WithholdingTax { get; set; } // Ennakonpidätys
        public decimal? EmployeePensionContribution { get; set; } // työntekijän TyEL-maksu
        public decimal? UnemploymentInsuranceContribution { get; set; } // Työttyömyysvakuutusmaksu
        public decimal? UnionMembershipFee { get; set; } // AY-jäsenmaksu
        
    }
}
