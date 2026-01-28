namespace WageWizard.DTOs
{
    public record EmployeesSummaryDto(
        Guid Id,
        Guid? UserId,
        string? FirstName,
        string? LastName,
        string? JobTitle,
        string? ImageUrl,
        string? Email
        );
}
