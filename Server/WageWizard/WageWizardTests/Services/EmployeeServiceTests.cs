using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Repositories;
using WageWizard.Services;

namespace WageWizardTests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEmployeeExists_ReturnsEmployee()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new EmployeeDto(
                Id: employeeId,
                FirstName: "Maija",
                LastName: "Virtanen",
                DateOfBirth: DateTime.Today.AddYears(-30),
                JobTitle: "Test",
                ImageUrl: null,
                Email: "test@test.fi",
                HomeAddress: "Katu 1",
                PostalCode: "00000",
                City: "Test",
                BankAccountNumber: "FI1234567890123456",
                TaxRate: 12.6m,
                GrossSalary: 3500.0m,
                StartDate: DateTime.Today
            );

            _employeeRepositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(employee);

            // Act
            var result = await _employeeService.GetByIdAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeId, result.Id);
            Assert.Equal("Maija", result.FirstName);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEmployeeDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _employeeRepositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync((EmployeeDto?)null);

            // Act
            Func<Task> act = () => _employeeService.GetByIdAsync(employeeId);

            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal($"Employee with ID {employeeId} not found.", exception.Message);
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_WhenEmployeesExist_ReturnsList()
        {
            // Arrange
            var employees = new List<EmployeesSummaryDto>
            {
                new EmployeesSummaryDto(Guid.NewGuid(), "Jack", "Sparrow", "Captain", null, "jack.sparrow@test.com"),
                new EmployeesSummaryDto(Guid.NewGuid(), "Jones", "Sparrow", "Villain", null, "jones.sparrow@test.com"),
                new EmployeesSummaryDto(Guid.NewGuid(), "James", "Bond", "private detective", null, "james.bond@test.com"),
            };

            _employeeRepositoryMock
                .Setup(r => r.GetEmployeesSummaryAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetEmployeesSummaryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(result, e => e.FirstName == "Jack");
            Assert.Contains(result, e => e.LastName == "Sparrow");
            Assert.Contains(result, e => e.JobTitle == "Captain");
            Assert.Contains(result, e => e.Email == "jack.sparrow@test.com");
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_WhenNoEmployeesExist_ThrowsNotFoundException()
        {
            // Arrange
            _employeeRepositoryMock
                .Setup(r => r.GetEmployeesSummaryAsync())
                .ReturnsAsync(Enumerable.Empty<EmployeesSummaryDto>());

            // Act
            Func<Task> act = () => _employeeService.GetEmployeesSummaryAsync();

            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal("No employees found.", exception.Message);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ValidDto_CreatesEmployee()
        {
            // Arrange
            var dto = new NewEmployeeRequestDto
            {
                FirstName = "Maija",
                LastName = "Virtanen",
                JobTitle = "Developer",
                Email = "maija.virtanen@example.com",
                HomeAddress = "Katu 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI1234567890123456",
                TaxRate = 20,
                MonthlySalary = 3000,
                StartDate = DateTime.Today,
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            Employee? addedEmployee = null;
            _employeeRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .Callback<Employee>(e => addedEmployee = e)
                .Returns(Task.CompletedTask);

            _employeeRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _employeeService.CreateEmployeeAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.FirstName, result.FirstName);
            Assert.Equal(dto.LastName, result.LastName);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.MonthlySalary, result.GrossSalary);
            Assert.Equal(dto.StartDate.Value, result.StartDate);
            Assert.Equal(dto.DateOfBirth.Value, result.DateOfBirth);

            _employeeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
            _employeeRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.Equal(result, addedEmployee);
        }

        [Fact]
        public async Task CreateEmployeeAsync_SaveChangesFails_ThrowsException()
        {
            // Arrange
            var dto = new NewEmployeeRequestDto
            {
                FirstName = "Maija",
                LastName = "Virtanen",
                JobTitle = "Developer",
                Email = "maija.virtanen@example.com",
                HomeAddress = "Katu 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI1234567890123456",
                TaxRate = 20,
                MonthlySalary = 3000,
                StartDate = DateTime.Today,
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            _employeeRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            _employeeRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ThrowsAsync(new InvalidOperationException("Database save failed"));

            // Act
            Func<Task> act = () => _employeeService.CreateEmployeeAsync(dto);

            // Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Equal("Database save failed", exception.Message);

            _employeeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
            _employeeRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
