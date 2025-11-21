namespace WageWizard.DTOs
{
    // Testattu ja tarkistettu
    public record EmployeesSummaryDto(
        Guid Id,
        string? FirstName,
        string? LastName,
        string? JobTitle,
        string? ImageUrl,
        string? Email
        );
}
