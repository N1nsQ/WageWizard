using Microsoft.EntityFrameworkCore;
using WageWizard.Controllers;
using WageWizard.Models;
using WageWizard.DTOs;
using WageWizard.Services;
using WageWizard.Utils;

namespace WageWizard.Repositories
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

            var age = EmployeeHelperFunctions.CalculateAge(employee.DateOfBirth);

            var tyelPercent = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, _payrollContext);
            var unemploymentPercent = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, _payrollContext);
            var taxPercent = employee.TaxPercentage ?? 0m;

            var result = PayrollServices.CollectSalaryStatementCalculations(
                employee.SalaryAmount ?? 0m,
                taxPercent,
                tyelPercent,
                unemploymentPercent
            )
            with
            {
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                GrossSalary = employee.SalaryAmount ?? 0m,
                TaxPercent = employee.TaxPercentage ?? 0m
            };

            return result;
        }

    }
}
