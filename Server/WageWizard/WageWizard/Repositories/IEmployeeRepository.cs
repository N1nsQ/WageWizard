using WageWizard.Domain.Entities;

namespace WageWizard.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee employee);
        Task<Employee?> FindDuplicateAsync(string FirstName, string LastName, string Email);
        Task UpdateAsync(Employee employee);
    } 
}
