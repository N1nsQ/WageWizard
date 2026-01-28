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

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return new EmployeeDto(
                employee.Id,
                employee.UserId,
                employee.FirstName,
                employee.LastName,
                employee.DateOfBirth,
                employee.JobTitle,
                employee.ImageUrl,
                employee.Email,
                employee.HomeAddress,
                employee.PostalCode,
                employee.City,
                employee.BankAccountNumber,
                employee.TaxRate,
                employee.GrossSalary,
                employee.StartDate
            );
        }

        public async Task<IEnumerable<EmployeeLookupDto>> GetLookupAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(e => new EmployeeLookupDto(
                e.Id,
                e.UserId,
                e.FirstName + " " + e.LastName
                ));
        }

        public async Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(e => new EmployeesSummaryDto(
                e.Id,
                e.UserId,
                e.FirstName,
                e.LastName,
                e.JobTitle,
                e.ImageUrl,
                e.Email
            ));
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(NewEmployeeRequestDto dto)
        {
            var duplicate = await _employeeRepository.FindDuplicateAsync(
               dto.FirstName,
               dto.LastName,
               dto.Email
            );

            if (duplicate != null)
            {
                throw new DuplicateEmployeeException("Employee with identical details already exists.");
            }

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName!,
                LastName = dto.LastName!,
                DateOfBirth = dto.DateOfBirth!.Value,
                JobTitle = dto.JobTitle!,
                ImageUrl = null,
                Email = dto.Email!,
                HomeAddress = dto.HomeAddress!,
                PostalCode = dto.PostalCode!,
                City = dto.City!,
                BankAccountNumber = dto.BankAccountNumber!,
                TaxRate = dto.TaxRate,
                GrossSalary = dto.MonthlySalary,
                StartDate = dto.StartDate!.Value,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _employeeRepository.AddAsync(employee);

            return new EmployeeDto
            (
                Guid.NewGuid(),
                Guid.NewGuid(),
                dto.FirstName,
                dto.LastName,
                dto.DateOfBirth!.Value,
                dto.JobTitle,
                null,
                dto.Email,
                dto.HomeAddress,
                dto.PostalCode,
                dto.City,
                dto.BankAccountNumber,
                dto.TaxRate,
                dto.MonthlySalary,
                dto.StartDate!.Value
            );

        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(Guid id, UpdateEmployeeRequestDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new EntityNotFoundException($"Employee with ID {id} not found.");
            }

            if (dto.HomeAddress != null) employee.HomeAddress = dto.HomeAddress;
            if (dto.PostalCode != null) employee.PostalCode = dto.PostalCode;
            if (dto.City != null) employee.City = dto.City;
            if (dto.BankAccountNumber != null) employee.BankAccountNumber = dto.BankAccountNumber;

            await _employeeRepository.UpdateAsync(employee);

            return new EmployeeDto(
                employee.Id,
                employee.UserId,
                employee.FirstName,
                employee.LastName,
                employee.DateOfBirth,
                employee.JobTitle,
                employee.ImageUrl,
                employee.Email,
                employee.HomeAddress,
                employee.PostalCode,
                employee.City,
                employee.BankAccountNumber,
                employee.TaxRate,
                employee.GrossSalary,
                employee.StartDate
                );
        }

        public async Task<EmployeeDto> UpdateEmployeeWithAdminRightsAsync(Guid id, UpdateEmployeeRequestWithAdminRightsDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new EntityNotFoundException($"Employee with ID {id} not found.");
            }

            if (dto.FirstName != null) employee.FirstName = dto.FirstName;
            if (dto.LastName != null) employee.LastName = dto.LastName;
            if (dto.DateOfBirth != null) employee.DateOfBirth = dto.DateOfBirth.Value;
            if (dto.JobTitle != null) employee.JobTitle = dto.JobTitle;
            if (dto.ImageUrl != null) employee.ImageUrl = dto.ImageUrl;
            if (dto.Email != null) employee.Email = dto.Email;
            if (dto.HomeAddress != null) employee.HomeAddress = dto.HomeAddress;
            if (dto.PostalCode != null) employee.PostalCode = dto.PostalCode;
            if (dto.City != null) employee.City = dto.City;
            if (dto.BankAccountNumber != null) employee.BankAccountNumber = dto.BankAccountNumber;
            if (dto.TaxRate != null) employee.TaxRate = dto.TaxRate.Value;
            if (dto.GrossSalary != null) employee.GrossSalary = dto.GrossSalary.Value;
            if (dto.StartDate != null) employee.StartDate = dto.StartDate.Value;

            await _employeeRepository.UpdateAsync(employee);

            return new EmployeeDto(
                employee.Id,
                employee.UserId,
                employee.FirstName,
                employee.LastName,
                employee.DateOfBirth,
                employee.JobTitle,
                employee.ImageUrl,
                employee.Email,
                employee.HomeAddress,
                employee.PostalCode,
                employee.City,
                employee.BankAccountNumber,
                employee.TaxRate,
                employee.GrossSalary,
                employee.StartDate
                );
        }
    }
}
