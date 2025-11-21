using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDto?> GetByIdAsync(Guid id); 
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync(); 
        Task AddAsync(Employee employee); 
        Task SaveChangesAsync();
    } 
}
