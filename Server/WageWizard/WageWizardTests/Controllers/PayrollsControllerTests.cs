using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard.Controllers;
using WageWizard.DTOs;
using WageWizard.Repositories.Interfaces;

namespace WageWizardTests.Controllers
{
    public class PayrollsControllerTests
    {
        [Fact]
        public async Task GetPayrollRatesAsync_WhenRepositoryReturnsData_ReturnsOkResult()
        {
            // Arrange
            var rates = new List<PayrollRatesDto>
        {
            new(2023, 0.0715m, 0.0865m, 0.0125m),
            new(2024, 0.072m, 0.087m, 0.013m)
        };

            var mockRepo = new Mock<IPayrollRepository>();
            mockRepo.Setup(r => r.GetPayrollRatesAsync()).ReturnsAsync(rates);

            var controller = new PayrollsController(mockRepo.Object);

            // Act
            var result = await controller.GetPayrollRatesAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRates = Assert.IsAssignableFrom<IEnumerable<PayrollRatesDto>>(okResult.Value);
            Assert.Equal(2, returnedRates.Count());
            mockRepo.Verify(r => r.GetPayrollRatesAsync(), Times.Once);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_WhenEmployeeExists_ReturnsOkResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var dto = new SalaryStatementCalculationDto(
                EmployeeId: employeeId,
                EmployeeName: "Anna Andersson",
                GrossSalary: 4000m,
                TaxPercent: 20m,
                WithholdingTax: 800m,
                TyELAmount: 286m,
                UnemploymentInsuranceAmount: 50m,
                NetSalary: 2864m
            );

            var mockRepo = new Mock<IPayrollRepository>();
            mockRepo.Setup(r => r.CalculateSalaryStatementAsync(employeeId))
                    .ReturnsAsync(dto);

            var controller = new PayrollsController(mockRepo.Object);

            // Act
            var result = await controller.CalculateSalaryStatementAsync(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDto = Assert.IsType<SalaryStatementCalculationDto>(okResult.Value);
            Assert.Equal(employeeId, returnedDto.EmployeeId);
            mockRepo.Verify(r => r.CalculateSalaryStatementAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_WhenEmployeeNotFound_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IPayrollRepository>();
            mockRepo.Setup(r => r.CalculateSalaryStatementAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((SalaryStatementCalculationDto?)null);

            var controller = new PayrollsController(mockRepo.Object);

            // Act
            var result = await controller.CalculateSalaryStatementAsync(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var error = Assert.IsType<ErrorResponseDto>(notFoundResult.Value);
            Assert.Equal("backend_error_messages.employee_not_found", error.Code);
        }
    }
}
