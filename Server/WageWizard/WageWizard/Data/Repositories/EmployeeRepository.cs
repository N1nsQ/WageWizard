using Microsoft.EntityFrameworkCore;
using WageWizard.Data;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Logic;
using WageWizard.DTOs;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PayrollContext _payrollContext;

        public EmployeeRepository(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
        {
            return await _payrollContext.Employees
                .OrderBy(e => e.LastName)
                .Select(e => new EmployeesSummaryDto
                (
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.ImageUrl,
                    e.Email
                ))
                .ToListAsync();
        }

        public async Task<EmployeeDetailsDto?> GetByIdAsync(Guid id)
        {
            return await _payrollContext.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDetailsDto
                (
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    AgeCalculator.CalculateAge(e.DateOfBirth),
                    e.JobTitle,
                    e.ImageUrl,
                    e.Email,
                    e.HomeAddress,
                    e.PostalCode,
                    e.City,
                    e.BankAccountNumber,
                    e.TaxPercentage,
                    e.SalaryAmount,
                    e.StartDate
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync()
        {
            var employees = await _payrollContext.Employees
                .OrderBy(e => e.LastName)
                .ToListAsync();

            var result = employees.Select(e =>
            {
                var age = AgeCalculator.CalculateAge(e.DateOfBirth);
                var tyelPercent = InsuranceRateCalculator.GetTyELPercent(age, DateTime.Now.Year, _payrollContext);
                var unemploymentInsurance = InsuranceRateCalculator.GetUnemploymentInsurancePercent(age, DateTime.Now.Year, _payrollContext);

                return new EmployeesSalaryDetailsDto
                (
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    age,
                    tyelPercent,
                    unemploymentInsurance,
                    e.TaxPercentage,
                    e.SalaryAmount
                );
            }).ToList();

            return result;
        }

        public async Task AddAsync(Employee employee)
        {
            await _payrollContext.Employees.AddAsync(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _payrollContext.SaveChangesAsync();
        }

    }
}
