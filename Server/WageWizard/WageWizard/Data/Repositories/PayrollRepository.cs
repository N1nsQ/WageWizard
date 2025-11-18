using Microsoft.EntityFrameworkCore;
using WageWizard.DTOs;
using WageWizard.Services;
using WageWizard.Data;
using WageWizard.Domain.Logic;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly PayrollContext _payrollContext;

        public PayrollRepository(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task<IEnumerable<PayrollRatesDto>> GetPayrollRatesAsync()
        {
            return await _payrollContext.PayrollRates
                .Select(r => new PayrollRatesDto
                (
                    r.Year,
                    r.TyEL_Basic,
                    r.TyEL_Senior,
                    r.UnemploymentInsurance
                ))
                .ToListAsync();
        }

        public async Task<SalaryStatementCalculationDto?> CalculateSalaryStatementAsync(Guid employeeId)
        {
            var employee = await _payrollContext.Employees.FindAsync(employeeId);
            if (employee == null)
                return null;

            var age = AgeCalculator.CalculateAge(employee.DateOfBirth);

            var tyelPercent = InsuranceRateCalculator.GetTyELPercent(age, DateTime.Now.Year, _payrollContext);
            var unemploymentPercent = InsuranceRateCalculator.GetUnemploymentInsurancePercent(age, DateTime.Now.Year, _payrollContext);
            var taxPercent = employee.TaxPercentage ?? 0m;

            var calc = PayrollServices.CollectSalaryStatementCalculations(
                employee.SalaryAmount ?? 0m,
                taxPercent,
                tyelPercent,
                unemploymentPercent
            );

            var result = new SalaryStatementCalculationDto(
                EmployeeId: employee.Id,
                EmployeeName: $"{employee.FirstName} {employee.LastName}",
                GrossSalary: employee.SalaryAmount ?? 0m,
                TaxPercent: employee.TaxPercentage ?? 0m,
                WithholdingTax: calc.WithholdingTax,
                TyELAmount: calc.TyELAmount,
                UnemploymentInsuranceAmount: calc.UnemploymentInsuranceAmount,
                NetSalary: calc.NetSalary
            );

            return result;
        }

    }
}
