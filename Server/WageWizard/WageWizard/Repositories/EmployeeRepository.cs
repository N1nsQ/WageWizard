using Microsoft.EntityFrameworkCore;
using WageWizard.DTOs;
using WageWizard.Models;
using WageWizard.Utils;

namespace WageWizard.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly PayrollContext _payrollContext;

        public EmployeeRepository(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _payrollContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
        {
            return await _payrollContext.Employees
                .OrderBy(e => e.LastName)
                .Select(e => new EmployeesSummaryDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    ImageUrl = e.ImageUrl,
                    Email = e.Email
                })
                .ToListAsync();
        }

        public async Task<EmployeeDetailsDto?> GetByIdAsync(Guid id)
        {
            return await _payrollContext.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDetailsDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Age = EmployeeHelperFunctions.CalculateAge(e.DateOfBirth),
                    JobTitle = e.JobTitle,
                    ImageUrl = e.ImageUrl,
                    Email = e.Email,
                    HomeAddress = e.HomeAddress,
                    PostalCode = e.PostalCode,
                    City = e.City,
                    BankAccountNumber = e.BankAccountNumber,
                    TaxPercentage = e.TaxPercentage,
                    SalaryAmount = e.SalaryAmount,
                    StartDate = e.StartDate,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync()
        {
            var employees = await _payrollContext.Employees
                .OrderBy(e => e.LastName)
                .ToListAsync();

            var result = employees.Select(e =>
            {
                var age = EmployeeHelperFunctions.CalculateAge(e.DateOfBirth);
                var tyelPercent = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, _payrollContext);
                var unemploymentInsurance = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, _payrollContext);

                return new EmployeesSalaryDetailsDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Age = age,
                    TyELPercent = tyelPercent,
                    UnemploymentInsurance = unemploymentInsurance,
                    TaxPercentage = e.TaxPercentage,
                    SalaryAmount = e.SalaryAmount
                };
            }).ToList();

            return result;
        }

        public async Task<EmployeesSalaryDetailsDto?> GetPayrollDetailsByIdAsync(Guid id)
        {
            var employee = await _payrollContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return null;

            var age = EmployeeHelperFunctions.CalculateAge(employee.DateOfBirth);
            var tyelPercent = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, _payrollContext);
            var unemploymentInsurance = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, _payrollContext);

            return new EmployeesSalaryDetailsDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = age,
                TyELPercent = tyelPercent,
                UnemploymentInsurance = unemploymentInsurance,
                TaxPercentage = employee.TaxPercentage,
                SalaryAmount = employee.SalaryAmount
            };
        }

    }
}
