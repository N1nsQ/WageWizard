using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeeLookupDto>> GetLookupAsync();
        Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync();
        Task<EmployeeDto> CreateEmployeeAsync(NewEmployeeRequestDto dto);
        Task<EmployeeDto> UpdateEmployeeAsync(Guid id, UpdateEmployeeRequestDto dto);
    }
}
