using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(NewEmployeeRequestDto dto);
    }
}
