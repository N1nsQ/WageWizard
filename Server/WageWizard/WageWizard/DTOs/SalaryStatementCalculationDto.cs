namespace WageWizard.DTOs
{
    public class SalaryStatementCalculationDto
    {
        // Employee information
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TaxPercent { get; set; }
       

        // Calculation results
        public decimal WithholdingTax {  get; set; }
        public decimal TyELAmount { get; set; }
        public decimal UnemploymentInsuranceAmount { get; set; }
        public decimal NetSalary { get; set; }
    }
}
