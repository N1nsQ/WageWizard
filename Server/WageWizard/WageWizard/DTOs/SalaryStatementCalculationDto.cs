namespace WageWizard.DTOs
{
    public record SalaryStatementCalculationDto(
        // Employee information
        Guid EmployeeId,
        string? EmployeeName,
        int Age,
        decimal GrossSalary,
        decimal TaxPercent,
        decimal TyELPercent,
        decimal UnemploymentInsurancePercent,
        // Calculation results
        decimal WithholdingTax,
        decimal TyELAmount,
        decimal UnemploymentInsuranceAmount,
        decimal NetSalary
        );
}
