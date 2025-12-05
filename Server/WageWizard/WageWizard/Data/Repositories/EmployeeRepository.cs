using Microsoft.EntityFrameworkCore;
using WageWizard.Domain.Entities;
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

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _payrollContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _payrollContext.Employees
                .OrderBy(e => e.LastName)
                .ToListAsync();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _payrollContext.Employees.Add(employee);
            await _payrollContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> FindDuplicateAsync(string FirstName, string LastName, string Email)
        {
            return await _payrollContext.Employees
                .FirstOrDefaultAsync(e =>
                e.FirstName == FirstName &&
                e.LastName == LastName &&
                e.Email == Email);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _payrollContext.Employees.Update(employee);
            await _payrollContext.SaveChangesAsync();
        }

    }
}
