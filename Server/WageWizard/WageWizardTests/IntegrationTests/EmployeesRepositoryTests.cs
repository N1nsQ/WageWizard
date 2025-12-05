using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WageWizard.Controllers;
using WageWizard.Data;
using WageWizard.Data.Repositories;
using WageWizard.Domain.Entities;
using WageWizard.Services;

namespace WageWizardTests.IntegrationTests
{
    public class EmployeesRepositoryTests
    {
        private readonly PayrollContext _context;
        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeService _employeeService;

        public EmployeesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PayrollContext(options);
            _employeeRepository = new EmployeeRepository(_context);
            _employeeService = new EmployeeService(_employeeRepository);

            _context.Employees.AddRange(
            new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Maija",
                LastName = "Virtanen",
                DateOfBirth = new DateTime(1990, 1, 1),
                JobTitle = "Developer",
                Email = "maija@test.fi",
                HomeAddress = "Katu 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI1234567890",
                TaxRate = 20,
                GrossSalary = 3500,
                StartDate = DateTime.Today
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Mikko",
                LastName = "Nieminen",
                DateOfBirth = new DateTime(1985, 5, 20),
                JobTitle = "Tester",
                Email = "mikko@test.fi",
                HomeAddress = "Katu 2",
                PostalCode = "00200",
                City = "Espoo",
                BankAccountNumber = "FI0987654321",
                TaxRate = 18,
                GrossSalary = 3000,
                StartDate = DateTime.Today
            }
        );
            _context.SaveChanges();
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectEmployeeDto()
        {
            var existingEmployee = _context.Employees.First();

            var result = await _employeeRepository.GetByIdAsync(existingEmployee.Id);

            Assert.NotNull(result);
            Assert.Equal(existingEmployee.Id, result!.Id);
            Assert.Equal(existingEmployee.FirstName, result.FirstName);
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_ReturnsAllEmployees()
        {
            var result = await _employeeService.GetEmployeesSummaryAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.FirstName == "Maija");
            Assert.Contains(result, e => e.FirstName == "Mikko");
        }

        [Fact]
        public async Task AddAsync_AddsEmployeeSuccessfully()
        {
            var newEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Liisa",
                LastName = "Korhonen",
                DateOfBirth = new DateTime(1992, 3, 15),
                JobTitle = "Designer",
                Email = "liisa@test.fi",
                HomeAddress = "Katu 3",
                PostalCode = "00300",
                City = "Vantaa",
                BankAccountNumber = "FI1122334455",
                TaxRate = 22,
                GrossSalary = 4000,
                StartDate = DateTime.Today
            };

            await _employeeRepository.AddAsync(newEmployee);

            var fetched = await _context.Employees.FindAsync(newEmployee.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Liisa", fetched!.FirstName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldPersistChangesToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            var repository = new EmployeeRepository(context);

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Maija",
                LastName = "Mehiläinen",
                HomeAddress = "Vanha katu 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI11"
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            // Act
            employee.HomeAddress = "Uusi katu 123";
            employee.City = "Espoo";

            await repository.UpdateAsync(employee);

            // Assert
            var updated = await context.Employees.FindAsync(employee.Id);

            Assert.NotNull(updated);
            Assert.Equal("Uusi katu 123", updated!.HomeAddress);
            Assert.Equal("Espoo", updated.City);

            // unchanged
            Assert.Equal("00100", updated.PostalCode);
        }
    }
}
