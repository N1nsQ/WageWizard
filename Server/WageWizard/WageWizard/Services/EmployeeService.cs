using WageWizard.Domain.Entities;
using WageWizard.DTOs;
using WageWizard.Repositories.Interfaces;
using WageWizard.Services.Interfaces;

namespace WageWizard.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateEmployeeAsync(NewEmployeeRequestDto dto)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                JobTitle = dto.JobTitle,
                ImageUrl = null,
                Email = dto.Email,
                HomeAddress = dto.HomeAddress,
                PostalCode = dto.PostalCode,
                City = dto.City,
                BankAccountNumber = dto.BankAccountNumber,
                TaxPercentage = dto.TaxRate,
                SalaryAmount = dto.MonthlySalary,
                StartDate = dto.StartDate,
                CreatedAt = DateTime.Today,
                UpdatedAt = DateTime.Today
                
            };

            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            return employee;

        }
    }
}
