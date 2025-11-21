using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class FailingEmployeeRepository : IEmployeeRepository
    {

        public Task<EmployeeDto?> GetByIdAsync(Guid id)
            => throw new RepositoryUnavailableException("Database not reachable");

        public Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
            => throw new RepositoryUnavailableException("Database not reachable");

        public Task AddAsync(Employee employee)
            => throw new RepositoryUnavailableException("Database not reachable");
        public Task SaveChangesAsync()
            => throw new RepositoryUnavailableException("Database not reachable");
    }
}
