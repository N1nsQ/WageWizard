using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class FailingEmployeeRepository : IEmployeeRepository
    {
        private static readonly string ErrorMessage = "Database not reachable";

        public Task<Employee?> GetByIdAsync(Guid id)
            => throw new RepositoryUnavailableException(ErrorMessage);

        public Task<IEnumerable<Employee>> GetAllAsync()
            => throw new RepositoryUnavailableException(ErrorMessage);

        public Task<Employee> AddAsync(Employee employee)
            => throw new RepositoryUnavailableException(ErrorMessage);
        public Task SaveChangesAsync()
            => throw new RepositoryUnavailableException(ErrorMessage);

        public Task<Employee?> FindDuplicateAsync(string FirstName, string LastName, string Email)
            => throw new RepositoryUnavailableException(ErrorMessage);

        public Task UpdateAsync(Employee employee)
            => throw new RepositoryUnavailableException(ErrorMessage);
    }
}
