namespace WageWizard.DTOs
{
    public record SalaryCalculationResultsDto
   (
        decimal WithholdingTax,
        decimal TyELAmount,
        decimal UnemploymentInsuranceAmount,
        decimal NetSalary
   );
}
