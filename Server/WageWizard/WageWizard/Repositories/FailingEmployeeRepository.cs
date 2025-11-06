using WageWizard.DTOs;
using WageWizard.Models;

namespace WageWizard.Repositories
{
    public class FailingEmployeeRepository : IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllAsync()
        => throw new Exception("Database not reachable");

        public Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
        => throw new Exception("Database not reachable");

        public Task<EmployeeDetailsDto?> GetByIdAsync(Guid id) 
            => throw new Exception("Database not reachable");

        public Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync()
            => throw new Exception("Database not reachable");

        public Task<EmployeesSalaryDetailsDto?> GetPayrollDetailsByIdAsync(Guid id)
            => throw new Exception("Database not reachable");
    }
}
