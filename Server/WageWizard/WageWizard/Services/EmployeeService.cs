using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
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
        public async Task<EmployeeDetailsDto?> GetByIdAsync(Guid id)
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

        public async Task<IEnumerable<EmployeesSalaryDetailsDto>> GetEmployeesSalaryPaymentDetailsAsync()
        {
            var employees = await _employeeRepository.GetEmployeesSalaryPaymentDetailsAsync();

            if (!employees.Any())
                throw new NotFoundException("No salary payment details found.");

            return employees;
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
