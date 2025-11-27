using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class FailingEmployeeRepository : IEmployeeRepository
    {
        private static readonly string ErrorMessage = "Database not reachable";

        public Task<EmployeeDto?> GetByIdAsync(Guid id)
            => throw new RepositoryUnavailableException(ErrorMessage);

        public Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
            => throw new RepositoryUnavailableException(ErrorMessage);

        public Task<Employee> AddAsync(Employee employee)
            => throw new RepositoryUnavailableException(ErrorMessage);
        public Task SaveChangesAsync()
            => throw new RepositoryUnavailableException(ErrorMessage);
    }
}
