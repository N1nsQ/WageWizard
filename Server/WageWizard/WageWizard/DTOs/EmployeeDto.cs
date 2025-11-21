namespace WageWizard.DTOs
{
    public record EmployeeDto
    (
        Guid Id,
        string? FirstName,
        string? LastName,
        DateTime DateOfBirth,
        string? JobTItle,
        string? ImageUrl,
        string? Email,
        string? HomeAddress,
        string? PostalCode,
        string? City,
        string? BankAccountNumber,
        decimal TaxRate,
        decimal GrossSalary,
        DateTime StartDate
    );
}
