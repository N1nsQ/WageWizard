namespace WageWizard.DTOs
{
    public record EmployeeDto
    (
        Guid Id,
        Guid? UserId,
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
