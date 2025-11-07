namespace WageWizard.DTOs
{
    public record EmployeesSummaryDto(
        Guid Id,
        string? FirstName,
        string? LastName,
        string? JobTitle,
        string? ImageUrl,
        string? Email
        );
}
