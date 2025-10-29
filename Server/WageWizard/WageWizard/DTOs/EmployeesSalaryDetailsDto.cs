namespace WageWizard.DTOs
{
    public class EmployeesSalaryDetailsDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? SalaryAmount { get; set; }
    }
}
