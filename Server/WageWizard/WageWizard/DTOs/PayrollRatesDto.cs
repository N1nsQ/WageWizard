namespace WageWizard.DTOs
{
    public record PayrollRatesDto(
       int Year,
       decimal TyEL_Basic,
       decimal TyEL_Senior,
       decimal UnemploymentInsurance
   );
}
