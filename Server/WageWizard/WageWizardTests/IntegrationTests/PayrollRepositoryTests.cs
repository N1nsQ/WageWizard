using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WageWizard.Data;
using WageWizard.Data.Repositories;
using WageWizard.Domain.Entities;

namespace WageWizardTests.IntegrationTests
{
    public class PayrollRepositoryTests
    {
        private readonly PayrollContext _context;
        private readonly PayrollsRepository _repository;

        public PayrollRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

            _context = new PayrollContext(options);
            _repository = new PayrollsRepository(_context);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Maija",
                LastName = "Virtanen",
                DateOfBirth = new DateTime(1990, 1, 1),
                JobTitle = "Developer",
                Email = "test@test.fi",
                GrossSalary = 3500,
                TaxRate = 20
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetEmployeeByIdAsync(employee.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(employee.Id);
            result.FirstName.Should().Be("Maija");
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            // Act
            var result = await _repository.GetEmployeeByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRatesForYearAsync_ShouldReturnRates_WhenYearExists()
        {
            // Arrange
            var rates = new PayrollRates
            {
                Year = 2025,
                TyEL_Basic = 0.0715m,
                TyEL_Senior = 0.0865m,
                UnemploymentInsurance = 0.0059m
            };

            _context.PayrollRates.Add(rates);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetRatesForYearAsync(2025);

            // Assert
            result.Should().NotBeNull();
            result!.Year.Should().Be(2025);
            result.TyEL_Basic.Should().Be(0.0715m);
        }

        [Fact]
        public async Task GetRatesForYearAsync_ShouldReturnNull_WhenYearDoesNotExist()
        {
            // Act
            var result = await _repository.GetRatesForYearAsync(2030);

            // Assert
            result.Should().BeNull();
        }

    }
}
