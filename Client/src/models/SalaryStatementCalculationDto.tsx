export interface SalaryStatementCalculationDto {
  employeeId: string;
  employeeName: string;
  grossSalary: number;
  taxPercent: number;
  withholdingTax: number;
  tyELAmount: number;
  unemploymentInsuranceAmount: number;
  netSalary: number;
}
