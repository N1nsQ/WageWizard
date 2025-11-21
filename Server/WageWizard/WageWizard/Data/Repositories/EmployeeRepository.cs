using Microsoft.EntityFrameworkCore;
using WageWizard.Domain.Entities;
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

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            return await _payrollContext.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                (
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.DateOfBirth,
                    e.JobTitle,
                    e.ImageUrl,
                    e.Email,
                    e.HomeAddress,
                    e.PostalCode,
                    e.City,
                    e.BankAccountNumber,
                    e.TaxRate,
                    e.GrossSalary,
                    e.StartDate
                ))
                .FirstOrDefaultAsync();
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
