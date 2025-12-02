export interface SalaryStatementCalculationDto {
  employeeId: string;
  employeeName: string;
  grossSalary: number;
  taxPercent: number;
  tyELPercent: number;
  unemploymentInsurancePercent: number;
  age: number;
  withholdingTax: number;
  tyELAmount: number;
  unemploymentInsuranceAmount: number;
  netSalary: number;
}
