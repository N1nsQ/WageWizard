using Microsoft.EntityFrameworkCore;
using WageWizard.Data;
using WageWizard.Domain.Entities;
using WageWizard.Repositories.Implementations;

namespace WageWizardTests.IntegrationTests
{
    public class PayrollRepositoryTests
    {
        private PayrollContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new PayrollContext(options);
        }

        [Fact]
        public async Task GetPayrollRatesAsync_ReturnsAllRates()
        {
            // Arrange
            using var context = CreateContext();
            context.PayrollRates.AddRange(
                new PayrollRates { Year = 2023, TyEL_Basic = 0.0715m, TyEL_Senior = 0.0865m, UnemploymentInsurance = 0.0125m },
                new PayrollRates { Year = 2024, TyEL_Basic = 0.072m, TyEL_Senior = 0.087m, UnemploymentInsurance = 0.013m }
            );
            await context.SaveChangesAsync();

            var repository = new PayrollRepository(context);

            // Act
            var result = await repository.GetPayrollRatesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Year == 2023);
            Assert.Contains(result, r => r.Year == 2024);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_WhenEmployeeExists_ReturnsCalculation()
        {
            // Arrange
            using var context = CreateContext();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Anna",
                LastName = "Andersson",
                DateOfBirth = new DateTime(1990, 1, 1),
                SalaryAmount = 4000m,
                TaxPercentage = 20m
            };

            context.Employees.Add(employee);

            context.PayrollRates.Add(new PayrollRates
            {
                Year = DateTime.Now.Year,
                TyEL_Basic = 0.0715m,
                TyEL_Senior = 0.0865m,
                UnemploymentInsurance = 0.0125m
            });

            await context.SaveChangesAsync();

            var repository = new PayrollRepository(context);

            // Act
            var result = await repository.CalculateSalaryStatementAsync(employee.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result!.EmployeeId);
            Assert.Equal("Anna Andersson", result.EmployeeName);
            Assert.Equal(4000m, result.GrossSalary);
            Assert.Equal(20m, result.TaxPercent);
            Assert.True(result.NetSalary > 0);
        }

        [Fact]
        public async Task CalculateSalaryStatementAsync_WhenEmployeeNotFound_ReturnsNull()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new PayrollRepository(context);

            // Act
            var result = await repository.CalculateSalaryStatementAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

    }
}
