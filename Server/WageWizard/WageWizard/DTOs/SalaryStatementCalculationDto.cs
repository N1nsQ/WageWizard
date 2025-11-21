namespace WageWizard.DTOs
{
    public record SalaryStatementCalculationDto(
        // Employee information
        Guid EmployeeId,
        string? EmployeeName,
        decimal GrossSalary,
        decimal TaxPercent,
        // Calculation results
        decimal WithholdingTax,
        decimal TyELAmount,
        decimal UnemploymentInsuranceAmount,
        decimal NetSalary
        );
}
