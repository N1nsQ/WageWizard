namespace WageWizard.DTOs
{
    public record EmployeesSalaryDetailsDto(
        Guid Id,
        string? FirstName,
        string? LastName,
        int? Age,
        decimal TyELPercent,
        decimal UnemploymentInsurance,
        decimal? TaxPercentage,
        decimal? SalaryAmount
        );
}
