namespace WageWizard.DTOs
{
    public record EmployeeLookupDto
    (
        Guid Id,
        Guid? UserId,
        string? FullName
    );
}
