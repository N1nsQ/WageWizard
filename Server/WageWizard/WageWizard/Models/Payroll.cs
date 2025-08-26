using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WageWizard.Models
{
    public class Payroll
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? SalaryDate { get; set; }
        public DateTime? PayPeriod { get; set; }

        // Salary & Benefits
        public decimal? BaseSalary { get; set; }
        public decimal? MealBenefit { get; set; }
        public decimal? PhoneBenefit { get; set; }
        public decimal? OvertimePay { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal? NetSalary { get; set; }

        // Deductions 
        public decimal? WithholdingTax { get; set; }
        public decimal? EmployeePensionContribution { get; set; }
        public decimal? UnemploymentInsuranceContribution { get; set; }
        public decimal? UnionMembershipFee { get; set; }
        
    }
}
