namespace WageWizard.DTOs
{
    public record UpdateEmployeeRequestWithAdminRightsDto
    (
        string? FirstName,
        string? LastName,
        DateTime? DateOfBirth,
        string? JobTitle,
        string? ImageUrl,
        string? Email,
        string? HomeAddress,
        string? PostalCode,
        string? City,
        string? BankAccountNumber,
        decimal? TaxRate,
        decimal? GrossSalary,
        DateTime? StartDate
   );
}
