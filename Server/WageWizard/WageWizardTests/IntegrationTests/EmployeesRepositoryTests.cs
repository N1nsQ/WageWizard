using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard.Controllers;
using WageWizard.DTOs;
using WageWizard.Models;
using WageWizard.Repositories;

namespace WageWizardTests.IntegrationTests
{
    public class EmployeesRepositoryTests
    {
        private static PayrollContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new PayrollContext(options);
        }

        [Fact]
        public void EmployeesController_CanBeConstructed_SetsRepository()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            var controller = new EmployeesController(mockRepo.Object);

            Assert.NotNull(controller);
            var field = typeof(EmployeesController)
                .GetField("_employeeRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.NotNull(field);
            var value = field!.GetValue(controller);
            Assert.Same(mockRepo.Object, value);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEmployees()
        {
            // Arrange
            using var context = CreateInMemoryContext();

            context.Employees.AddRange(
                new Employee { Id = Guid.NewGuid(), FirstName = "Matti", LastName = "Meikäläinen", JobTitle = "Developer" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Maija", LastName = "Mallikas", JobTitle = "Designer" }
            );
            await context.SaveChangesAsync();

            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.FirstName == "Matti");
            Assert.Contains(result, e => e.FirstName == "Maija");
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_ReturnsSummaryList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: "PayrollDb_SummaryTest")
                .Options;

            using var context = new PayrollContext(options);
            context.Employees.AddRange(
                new Employee { Id = Guid.NewGuid(), FirstName = "Anna", LastName = "Andersson", Email = "anna@example.com", JobTitle = "Developer" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Bertil", LastName = "Berg", Email = "bertil@example.com", JobTitle = "Designer" }
            );
            await context.SaveChangesAsync();

            var repository = new EmployeeRepository(context);

            // Act
            var result = await repository.GetEmployeesSummaryAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, dto => Assert.False(string.IsNullOrEmpty(dto.FirstName)));
            Assert.All(result, dto => Assert.False(string.IsNullOrEmpty(dto.LastName)));
            Assert.All(result, dto => Assert.False(string.IsNullOrEmpty(dto.Email)));
            Assert.All(result, dto => Assert.False(string.IsNullOrEmpty(dto.JobTitle)));
        }

        [Fact]
        public async Task GetEmployyesSummaryAsync_WhenEmployeeDoesNotExist_ReturnsNotFound()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            var repository = new EmployeeRepository(context);
            var controller = new EmployeesController(repository);

            var result = await controller.GetEmployeesSummaryAsync();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var error = Assert.IsType<ErrorResponseDto>(notFoundResult.Value);
            Assert.Equal("backend_error_messages.employees_not_found", error.Code);
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_WhenRepositoryThrows_ReturnsInternalServerError()
        {
            // Arrange
            var repository = new FailingEmployeeRepository();
            var controller = new EmployeesController(repository);

            // Act
            var result = await controller.GetEmployeesSummaryAsync();

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusResult.StatusCode);

            var message = Assert.IsType<string>(statusResult.Value);
            Assert.Contains("Database connection error", message);
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_WhenRepositoryReturnsData_ReturnsOkResult()
        {
            // Arrange
            var employees = new List<EmployeesSummaryDto>
        {
            new EmployeesSummaryDto
            (
                Guid.NewGuid(),
                "Anna",
                "Andersson",
                "Developer",
                "",
                "anna@example.com"
                
            ),
            new EmployeesSummaryDto
            (
                Guid.NewGuid(),
                "Bertil",
                "Berg",
                "Designer",
                "",
                "bertil@example.com"
            )
        };

                var mockRepo = new Mock<IEmployeeRepository>();
                mockRepo.Setup(r => r.GetEmployeesSummaryAsync()).ReturnsAsync(employees);

                var controller = new EmployeesController(mockRepo.Object);

                // Act
                var result = await controller.GetEmployeesSummaryAsync();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeesSummaryDto>>(okResult.Value);
                Assert.Equal(2, returnedEmployees.Count());
                Assert.Contains(returnedEmployees, e => e.FirstName == "Anna");
                Assert.Contains(returnedEmployees, e => e.FirstName == "Bertil");

                mockRepo.Verify(r => r.GetEmployeesSummaryAsync(), Times.Once);
            }

        [Fact]
        public async Task GetByIdAsync_WhenEmployeeExists_ReturnsEmployeeDetails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Anna",
                LastName = "Virtanen",
                DateOfBirth = new DateTime(1990, 1, 1),
                JobTitle = "Developer",
                Email = "anna@example.com"
            };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            var repository = new EmployeeRepository(context);
            var controller = new EmployeesController(repository);

            // Act
            var result = await controller.GetByIdAsync(employee.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<EmployeeDetailsDto>(okResult.Value);

            Assert.Equal(employee.Id, dto.Id);
            Assert.Equal("Anna", dto.FirstName);
            Assert.Equal("Virtanen", dto.LastName);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEmployeeDoesNotExist_ReturnsNotFound()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            var repository = new EmployeeRepository(context);
            var controller = new EmployeesController(repository);

            var result = await controller.GetByIdAsync(Guid.NewGuid());

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var error = Assert.IsType<ErrorResponseDto>(notFoundResult.Value);
            Assert.Equal("backend_error_messages.employees_not_found", error.Code);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRepositoryThrows_ReturnsInternalServerError()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(new Exception("Simulated failure"));

            var controller = new EmployeesController(mockRepo.Object);

            var result = await controller.GetByIdAsync(Guid.NewGuid());

            var statusResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusResult.StatusCode);
            var message = Assert.IsType<string>(statusResult.Value);
            Assert.Contains("Database connection error", message);
        }

        [Fact]
        public async Task GetEmployeesSalaryPaymentDetails_WhenEmployeesExist_ReturnsList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            context.Employees.AddRange(
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Anna",
                    LastName = "Virtanen",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    TaxPercentage = 20,
                    SalaryAmount = 3500m
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mikko",
                    LastName = "Laine",
                    DateOfBirth = new DateTime(1985, 1, 1),
                    TaxPercentage = 25,
                    SalaryAmount = 4000m
                }
            );

            context.PayrollRates.Add(new PayrollRates
            {
                Year = DateTime.Now.Year,
                TyEL_Basic = 0.0715m,
                TyEL_Senior = 0.0865m,
                UnemploymentInsurance = 0.0125m
            });

            await context.SaveChangesAsync();

            var repository = new EmployeeRepository(context);
            var controller = new EmployeesController(repository);

            // Act
            var result = await controller.GetEmployeesSalaryPaymentDetailsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dtoList = Assert.IsAssignableFrom<IEnumerable<EmployeesSalaryDetailsDto>>(okResult.Value);
            Assert.Equal(2, dtoList.Count());

            Assert.All(dtoList, dto =>
            {
                Assert.InRange(dto.TyELPercent, 0, 0.1m);
                Assert.InRange(dto.UnemploymentInsurancePercent, 0, 0.1m);
            });
        }

        [Fact]
        public async Task GetEmployeesSalaryPaymentDetails_WhenNoEmployees_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            var repository = new EmployeeRepository(context);
            var controller = new EmployeesController(repository);

            // Act
            var result = await controller.GetEmployeesSalaryPaymentDetailsAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var error = Assert.IsType<ErrorResponseDto>(notFoundResult.Value);
            Assert.Equal("backend_error_messages.employees_not_found", error.Code);
        }

        [Fact]
        public async Task GetEmployeesSalaryPaymentDetails_WhenRepositoryThrows_ReturnsInternalServerError()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetEmployeesSalaryPaymentDetailsAsync())
                .ThrowsAsync(new Exception("Simulated database failure"));

            var controller = new EmployeesController(mockRepo.Object);

            // Act
            var result = await controller.GetEmployeesSalaryPaymentDetailsAsync();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            var error = Assert.IsType<ErrorResponseDto>(objectResult.Value);
            Assert.Equal("Database connection error: ", error.Code);
            Assert.Contains("Simulated database failure", error.Message);
        }

        [Fact]
        public async Task GetPayrollDetailsById_WhenEmployeeExists_ReturnsDto()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new PayrollContext(options);
            var employeeId = Guid.NewGuid();

            context.Employees.Add(new Employee
            {
                Id = employeeId,
                FirstName = "Anna",
                LastName = "Virtanen",
                DateOfBirth = new DateTime(1990, 1, 1),
                TaxPercentage = 20,
                SalaryAmount = 3500m
            });

            context.PayrollRates.Add(new PayrollRates
            {
                Year = DateTime.Now.Year,
                TyEL_Basic = 0.0715m,
                TyEL_Senior = 0.0865m,
                UnemploymentInsurance = 0.0125m
            });

            await context.SaveChangesAsync();

            var repository = new EmployeeRepository(context);
            var controller = new EmployeesController(repository);

            // Act
            var result = await controller.GetPayrollDetailsByIdAsync(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<EmployeesSalaryDetailsDto>(okResult.Value);

            Assert.Equal(employeeId, dto.Id);
            Assert.Equal("Anna", dto.FirstName);
            Assert.Equal("Virtanen", dto.LastName);
            Assert.InRange(dto.TyELPercent, 0, 0.1m);
            Assert.InRange(dto.UnemploymentInsurancePercent, 0, 0.1m);
        }

        [Fact]
        public async Task GetPayrollDetailsByIdAsync_WhenEmployeeNotFound_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetPayrollDetailsByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((EmployeesSalaryDetailsDto?)null); 

            var controller = new EmployeesController(mockRepo.Object);

            // Act
            var result = await controller.GetPayrollDetailsByIdAsync(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var error = Assert.IsType<ErrorResponseDto>(notFoundResult.Value);
            Assert.Equal("backend_error_messages.employees_not_found", error.Code);
        }

        [Fact]
        public async Task GetPayrollDetailsByIdAsync_WhenRepositoryThrows_ReturnsInternalServerError()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetPayrollDetailsByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Simulated database failure"));

            var controller = new EmployeesController(mockRepo.Object);

            // Act
            var result = await controller.GetPayrollDetailsByIdAsync(Guid.NewGuid());

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            var error = Assert.IsType<ErrorResponseDto>(objectResult.Value);
            Assert.Equal("Database connection error: ", error.Code);
            Assert.Contains("Simulated database failure", error.Message);
        }

    }
}
