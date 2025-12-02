using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WageWizard.Controllers;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizardTests.Controllers
{
    public class PayrollsControllerTests
    {
        private readonly Mock<IPayrollsService> _payrollsServiceMock;
        private readonly PayrollsController _controller;

        public PayrollsControllerTests()
        {
            _payrollsServiceMock = new Mock<IPayrollsService>();
            _controller = new PayrollsController(_payrollsServiceMock.Object);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_ShouldReturnOk_WithSalaryStatement()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedDto = new SalaryStatementCalculationDto(
                EmployeeId: employeeId,
                EmployeeName: "Maija Virtanen",
                GrossSalary: 3500,
                Age: 35,
                TaxPercent: 20,
                WithholdingTax: 700,
                TyELAmount: 245,
                UnemploymentInsuranceAmount: 52.5m,
                NetSalary: 2502.5m
            );

            _payrollsServiceMock
                .Setup(s => s.CalculateSalaryStatementAsync(employeeId))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _controller.CalculateSalaryStatementAsync(employeeId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(expectedDto);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_ShouldThrow_WhenServiceThrows()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            _payrollsServiceMock
                .Setup(s => s.CalculateSalaryStatementAsync(employeeId))
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            Func<Task> act = async () => await _controller.CalculateSalaryStatementAsync(employeeId);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Something went wrong");
        }
    }
}
