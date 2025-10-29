namespace WageWizard.DTOs
{
    public class EmployeeDetailsDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public string? HomeAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? BankAccountNumber { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? SalaryAmount { get; set; }
        public DateTime? StartDate { get; set; }

    }
}
