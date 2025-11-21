using WageWizard.Domain.Exceptions;
using WageWizard.Domain.Logic;
using WageWizard.DTOs;
using WageWizard.Repositories;
using WageWizard.Services.Interfaces;

namespace WageWizard.Services
{
    public class PayrollsService : IPayrollsService
    {
        private readonly IPayrollsRepository _payrollsRepository;

        public PayrollsService(IPayrollsRepository payrollsRepository)
        {
            _payrollsRepository = payrollsRepository;
        }


        public async Task<SalaryStatementCalculationDto> CalculateSalaryStatementAsync(Guid employeeId)
        {
            var employee = await _payrollsRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
                throw new EntityNotFoundException($"Employee with id {employeeId} not found.");

            int age = AgeCalculator.CalculateAge(employee.DateOfBirth);

            int currentYear = DateTime.Now.Year;
            var rates = await _payrollsRepository.GetRatesForYearAsync(currentYear);

            if (rates == null)
                throw new EntityNotFoundException($"No payroll rates found for year {currentYear}.");

            decimal tyelPercent = InsuranceRateCalculator.GetTyELPercent(age, rates);

            decimal unempPercent = InsuranceRateCalculator.GetUnemploymentInsurancePercent(age, rates);

            decimal gross = employee.GrossSalary;
            decimal taxPercent = employee.TaxRate;

            decimal withholding = SalaryCalculator.CalculateWithholdingTaxAmount(gross, taxPercent);
            decimal tyel = SalaryCalculator.CalculateTyELAmount(gross, tyelPercent);
            decimal unemployment = SalaryCalculator.CalculateUnemploymentInsuranceAmount(gross, unempPercent);
            decimal net = SalaryCalculator.CalculateNetSalaryAmount(gross, taxPercent, tyelPercent, unempPercent);

            return new SalaryStatementCalculationDto(
                EmployeeId: employee.Id,
                EmployeeName: $"{employee.FirstName} {employee.LastName}",
                GrossSalary: gross,
                TaxPercent: taxPercent,
                WithholdingTax: withholding,
                TyELAmount: tyel,
                UnemploymentInsuranceAmount: unemployment,
                NetSalary: net
            );
        }
 
    }
}
