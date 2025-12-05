namespace WageWizard.DTOs
{
    public record UpdateEmployeeRequestDto
    (
        string? HomeAddress,
        string? PostalCode,
        string? City,
        string? BankAccountNumber
    );
}
