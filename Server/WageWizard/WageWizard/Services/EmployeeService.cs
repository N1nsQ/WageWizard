using WageWizard.Data.Repositories;
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
        private readonly IUserRepository _userRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
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

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Email,
                Email = dto.Email,
                RoleId = UserRole.Employee,
            };

            await _userRepository.AddAsync(user);

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
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
                employee.Id,
                employee.UserId,
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

            if (!string.IsNullOrWhiteSpace(dto.HomeAddress))
                employee.HomeAddress = dto.HomeAddress;
            if (!string.IsNullOrWhiteSpace(dto.HomeAddress))
                employee.HomeAddress = dto.HomeAddress;
            if (!string.IsNullOrWhiteSpace(dto.City))
                employee.City = dto.City;
            if (!string.IsNullOrWhiteSpace(dto.BankAccountNumber))
                employee.BankAccountNumber = dto.BankAccountNumber;

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

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                employee.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                employee.LastName = dto.LastName;

            if (dto.DateOfBirth.HasValue)
                employee.DateOfBirth = dto.DateOfBirth.Value;

            if (!string.IsNullOrWhiteSpace(dto.JobTitle))
                employee.JobTitle = dto.JobTitle;

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
                employee.ImageUrl = dto.ImageUrl;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                employee.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.HomeAddress))
                employee.HomeAddress = dto.HomeAddress;

            if (!string.IsNullOrWhiteSpace(dto.PostalCode))
                employee.PostalCode = dto.PostalCode;

            if (!string.IsNullOrWhiteSpace(dto.City))
                employee.City = dto.City;

            if (!string.IsNullOrWhiteSpace(dto.BankAccountNumber))
                employee.BankAccountNumber = dto.BankAccountNumber;

            if (dto.TaxRate.HasValue)
                employee.TaxRate = dto.TaxRate.Value;

            if (dto.GrossSalary.HasValue)
                employee.GrossSalary = dto.GrossSalary.Value;

            if (dto.StartDate.HasValue)
                employee.StartDate = dto.StartDate.Value;

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
