using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WageWizard.DTOs;
using WageWizard.Models;
using WageWizard.Services;
using WageWizard.Utils;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollsController(PayrollContext context) : ControllerBase
    {
        private readonly PayrollContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payroll>>> GetPayrolls()
        {
            return await _context.Payrolls.ToListAsync();
        }

        [HttpGet("PayrollRates")]
        public async Task<ActionResult<IEnumerable<PayrollRates>>> GetTyELRates()
        {
            return await _context.PayrollRates.ToListAsync();
        }

        [HttpGet("SalaryStatement")]
        public async Task<ActionResult<SalaryStatementCalculationDto>> CalculateSalaryStatement(Guid employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound(new
                {
                    Code = "backend_error_messages.employee_not_found"
                });
            }

            var age = EmployeeHelperFunctions.CalculateAge(employee.DateOfBirth);

            var tyelPercent = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, _context);
            var unemploymentPercent = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, _context);
            var taxPercent = employee.TaxPercentage;

            var result = PayrollServices.CollectSalaryStatementCalculations(
                employee.SalaryAmount ?? 0m,
                taxPercent ?? 0m,
                tyelPercent,
                unemploymentPercent
            );

            result.EmployeeId = employee.Id;
            result.EmployeeName = $"{employee.FirstName} {employee.LastName}";
            result.GrossSalary = employee.SalaryAmount ?? 0m;
            result.TaxPercent = employee.TaxPercentage ?? 0m;

            return Ok(result);
        }
    }
}
