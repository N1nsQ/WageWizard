using WageWizard.Domain.Entities;
using WageWizard.DTOs;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class FailingEmployeeRepository : IEmployeeRepository
    {

        public Task<EmployeeDto?> GetByIdAsync(Guid id)
            => throw new Exception("Database not reachable");

        public Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
        => throw new Exception("Database not reachable");

        public Task AddAsync(Employee employee)
            => throw new Exception("Database not reachable");
        public Task SaveChangesAsync()
            => throw new Exception("Database not reachable");
    }
}
