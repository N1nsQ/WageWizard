namespace WageWizard.DTOs
{
    public record EmployeeDetailsDto(
        Guid Id,
        string? FirstName,
        string? LastName,
        int? Age,
        string? JobTitle,
        string? ImageUrl,
        string? Email,
        string? HomeAddress,
        string? PostalCode,
        string? City,
        string? BankAccountNumber,
        decimal? TaxPercentage,
        decimal? SalaryAmount,
        DateTime? StartDate
        );
}
