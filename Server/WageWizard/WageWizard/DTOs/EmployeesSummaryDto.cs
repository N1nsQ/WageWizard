namespace WageWizard.DTOs
{
    public class EmployeesSummaryDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
    }
}
