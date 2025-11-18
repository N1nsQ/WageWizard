using System.ComponentModel.DataAnnotations;

namespace WageWizard.Domain.Entities
{
    public class PayrollRates
    {
        [Key]
        public int Year { get; set; }
        public decimal TyEL_Basic { get; set; }
        public decimal TyEL_Senior { get; set; }
        public decimal UnemploymentInsurance { get; set; }
    }
}
