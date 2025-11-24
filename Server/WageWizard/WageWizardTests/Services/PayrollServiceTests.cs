using FluentAssertions;
using Moq;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.Repositories;
using WageWizard.Services;

namespace WageWizardTests.Services
{
    public class PayrollServiceTests
    {
        private readonly Mock<IPayrollsRepository> _payrollsRepositoryMock;
        private readonly PayrollsService _payrollsService;

        public PayrollServiceTests()
        {
            _payrollsRepositoryMock = new Mock<IPayrollsRepository>();
            _payrollsService = new PayrollsService(_payrollsRepositoryMock.Object);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_ShouldReturnCorrectDto_WhenDataIsValid()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "Maija",
                LastName = "Virtanen",
                GrossSalary = 3500m,
                TaxRate = 20m,
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            var rates = new PayrollRates
            {
                Year = 2025,
                TyEL_Basic = 0.0715m,
                TyEL_Senior = 0.0865m,
                UnemploymentInsurance = 0.0059m
            };

            _payrollsRepositoryMock.Setup(r => r.GetEmployeeByIdAsync(employeeId))
                .ReturnsAsync(employee);

            _payrollsRepositoryMock.Setup(r => r.GetRatesForYearAsync(It.IsAny<int>()))
                .ReturnsAsync(rates);

            // Act
            var result = await _payrollsService.CalculateSalaryStatementAsync(employeeId);

            // Assert
            result.Should().NotBeNull();
            result.EmployeeId.Should().Be(employeeId);
            result.EmployeeName.Should().Be("Maija Virtanen");
            result.GrossSalary.Should().Be(employee.GrossSalary);
            result.TaxPercent.Should().Be(employee.TaxRate);
            result.NetSalary.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_ShouldThrowEntityNotFoundException_WhenEmployeeNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _payrollsRepositoryMock.Setup(r => r.GetEmployeeByIdAsync(employeeId))
                .ReturnsAsync((Employee?)null);

            // Act
            Func<Task> act = async () => await _payrollsService.CalculateSalaryStatementAsync(employeeId);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException>()
                .WithMessage($"Employee with id {employeeId} not found.");
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_ShouldThrowEntityNotFoundException_WhenRatesNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "Maija",
                LastName = "Virtanen",
                GrossSalary = 3500m,
                TaxRate = 20m,
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            _payrollsRepositoryMock.Setup(r => r.GetEmployeeByIdAsync(employeeId))
                .ReturnsAsync(employee);

            _payrollsRepositoryMock.Setup(r => r.GetRatesForYearAsync(It.IsAny<int>()))
                .ReturnsAsync((PayrollRates?)null);

            // Act
            Func<Task> act = async () => await _payrollsService.CalculateSalaryStatementAsync(employeeId);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException>()
                .WithMessage($"No payroll rates found for year {DateTime.Now.Year}.");
        }
    }
    
}
