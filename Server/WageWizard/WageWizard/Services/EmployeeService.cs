using WageWizard.Data.Repositories;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.Domain.Logic;
using WageWizard.DTOs;
using WageWizard.Repositories;
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

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                throw new NotFoundException($"Employee with ID {id} not found.");

            return employee;
        }

        public async Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
        {
            var employees = await _employeeRepository.GetEmployeesSummaryAsync();

            if (!employees.Any())
                throw new NotFoundException("No employees found.");

            return employees;
        }

        public async Task<Employee> CreateEmployeeAsync(NewEmployeeRequestDto dto)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth!.Value,
                JobTitle = dto.JobTitle,
                ImageUrl = null,
                Email = dto.Email,
                HomeAddress = dto.HomeAddress,
                PostalCode = dto.PostalCode,
                City = dto.City,
                BankAccountNumber = dto.BankAccountNumber,
                TaxRate = dto.TaxRate,
                GrossSalary = dto.MonthlySalary,
                StartDate = dto.StartDate!.Value,
                CreatedAt = DateTime.Today,
                UpdatedAt = DateTime.Today
                
            };

            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            return employee;

        }
    }
}
