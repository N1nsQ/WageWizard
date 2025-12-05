using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WageWizard.Controllers;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizardTests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly EmployeesController _employeeController;
        
        public EmployeesControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeesController(_employeeServiceMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOk_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedEmployeeDto = new EmployeeDto(
                employeeId,
                "Maija",
                "Mehiläinen",
                DateTime.Parse("1990-01-01"),
                "Developer",
                null,
                "maija@example.com",
                "Katuosoite 1",
                "00100",
                "Helsinki",
                "FI1234567890123456",
                20,
                3000m,
                DateTime.Parse("2020-01-01")
            );

            _employeeServiceMock
                .Setup(s => s.GetByIdAsync(employeeId))
                .ReturnsAsync(expectedEmployeeDto);

            // Act
            var result = await _employeeController.GetByIdAsync(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDto = Assert.IsType<EmployeeDto>(okResult.Value);

            Assert.Equal(expectedEmployeeDto.Id, returnedDto.Id);
            Assert.Equal(expectedEmployeeDto.FirstName, returnedDto.FirstName);
            Assert.Equal(expectedEmployeeDto.LastName, returnedDto.LastName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldPropagateException_WhenEmployeeNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _employeeServiceMock.Setup(s => s.GetByIdAsync(employeeId))
                .ThrowsAsync(new EntityNotFoundException($"Employee with id {employeeId} not found."));

            // Act
            Func<Task> act = async () => await _employeeController.GetByIdAsync(employeeId);

            // Assert
            await act.Should().ThrowAsync<EntityNotFoundException>()
                .WithMessage($"Employee with id {employeeId} not found.");
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_ShouldReturnOk()
        {
            // Arrange
            var summary = new List<EmployeesSummaryDto>
            {
                new EmployeesSummaryDto(Guid.NewGuid(), "Jack", "Sparrow", "Captain", null, "jack.sparrow@test.com"),
                new EmployeesSummaryDto(Guid.NewGuid(), "Jones", "Sparrow", "Villain", null, "jones.sparrow@test.com"),
                new EmployeesSummaryDto(Guid.NewGuid(), "James", "Bond", "private detective", null, "james.bond@test.com"),
            };

            _employeeServiceMock
                .Setup(s => s.GetEmployeesSummaryAsync())
                .ReturnsAsync(summary);

            // Act
            var result = await _employeeController.GetEmployeesSummaryAsync();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(summary);
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_ShouldPropagateException_WhenServiceThrows()
        {
            // Arrange
            _employeeServiceMock
                .Setup(s => s.GetEmployeesSummaryAsync())
                .ThrowsAsync(new Exception("Database not reachable"));

            // Act
            Func<Task> act = async () => await _employeeController.GetEmployeesSummaryAsync();

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Database not reachable");
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnOk()
        {
            // Arrange
            var newEmployeeDto = new NewEmployeeRequestDto
            {
                FirstName = "Matti",
                LastName = "Meikäläinen",
                JobTitle = "Ohjelmoija",
                Email = "matti@example.com",
                HomeAddress = "Katu 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI1234567890123456",
                TaxRate = 30,
                MonthlySalary = 3000,
                StartDate = DateTime.Today,
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            var createdEmployeeDto = new EmployeeDto(
                Guid.NewGuid(),
                newEmployeeDto.FirstName,
                newEmployeeDto.LastName,
                newEmployeeDto.DateOfBirth!.Value,
                newEmployeeDto.JobTitle!,
                null,
                newEmployeeDto.Email!,
                newEmployeeDto.HomeAddress!,
                newEmployeeDto.PostalCode!,
                newEmployeeDto.City!,
                newEmployeeDto.BankAccountNumber!,
                newEmployeeDto.TaxRate,
                newEmployeeDto.MonthlySalary,
                newEmployeeDto.StartDate!.Value
            );

            _employeeServiceMock
                .Setup(s => s.CreateEmployeeAsync(It.IsAny<NewEmployeeRequestDto>()))
                .ReturnsAsync(createdEmployeeDto);

            // Act
            var result = await _employeeController.CreateEmployee(newEmployeeDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEmployee = Assert.IsType<EmployeeDto>(okResult.Value);

            Assert.Equal(newEmployeeDto.FirstName, returnedEmployee.FirstName);
            Assert.Equal(newEmployeeDto.LastName, returnedEmployee.LastName);
            Assert.Equal(newEmployeeDto.Email, returnedEmployee.Email);
        }


    }
}
