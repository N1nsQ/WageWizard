using System.Diagnostics;

namespace WageWizard.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? HomeAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? BankAccountNumber { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? SalaryAmount { get; set; }
        public DateTime? StartDate { get; set; } // The employee's official start date at the company
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
