namespace WageWizard.DTOs
{
    public record NewEmployeeRequestDto(
        string FirstName,
        string LastName,
        string JobTitle,
        string Email,
        string HomeAddress,
        string PostalCode,
        string City,
        string BankAccountNumber,
        decimal TaxRate,
        decimal MonthlySalary,
        DateTime StartDate,
        DateTime DateOfBirth
        );
}
