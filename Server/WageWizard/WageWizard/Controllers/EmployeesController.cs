using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WageWizard.DTOs;
using WageWizard.Models;
using WageWizard.Utils;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(PayrollContext context) : ControllerBase
    {
        private readonly PayrollContext _context = context;

        const string employeesNotFound = "backend_error_messages.employees_not_found";
        const string databaseConnectionError = "Database connection error: ";

        // Haetaan kaikki työntekijät
        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // Tiedot Työntekijät-sivulle taulukkoon, lyhyt kooste
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<EmployeesSummaryDto>>> GetEmployeesSummary()
        {
            

            var employees = await _context.Employees
                .OrderBy(e => e.LastName)
                .Select(e => new EmployeesSummaryDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    ImageUrl = e.ImageUrl,
                    Email = e.Email
                })
                .ToListAsync();

            if (employees == null)
            {
                var error = new ErrorResponseDto
                {
                    Code = employeesNotFound
                };

            return NotFound(error);

            }

            return Ok(employees);
        }

        // Haetaan yksittäisen työntekijän tiedot Työntekijä-sivun näkymään
        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<EmployeeDetailsDto>>> GetEmployeeDetails(Guid id)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDetailsDto
                {
                    Id=e.Id,
                    FirstName=e.FirstName,
                    LastName=e.LastName,
                    Age = EmployeeHelperFunctions.CalculateAge(e.DateOfBirth),
                    JobTitle =e.JobTitle,
                    ImageUrl = e.ImageUrl,
                    Email = e.Email,
                    HomeAddress = e.HomeAddress,
                    PostalCode = e.PostalCode,
                    City = e.City,
                    BankAccountNumber = e.BankAccountNumber,
                    TaxPercentage = e.TaxPercentage,
                    SalaryAmount = e.SalaryAmount,
                    StartDate = e.StartDate,
                 })
                 .FirstOrDefaultAsync();

            if (employee == null)
            {
                var error = new ErrorResponseDto
                {
                    Code = employeesNotFound
                };

                return NotFound(error);

            }

            return Ok(employee);
        }

        // Haentaan kaikkien työntekijöiden tiedot palkanlaskentaa varten
        [HttpGet("paymentDetails")]
        public async Task<ActionResult<IEnumerable<EmployeesSalaryDetailsDto>>> GetEmployeesSalaryPaymentDetails()
        {
            try
            {
                var employees = await _context.Employees
                .OrderBy(e => e.LastName)
                .ToListAsync();

                if (employees.Count == 0)
                {
                    var error = new ErrorResponseDto
                    {
                        Code = employeesNotFound
                    };

                    return NotFound(error);

                }

                var result = employees.Select(e =>
                {
                    var age = EmployeeHelperFunctions.CalculateAge(e.DateOfBirth);
                    var tyelPercent = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, _context);
                    var unemploymentInsurance = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, _context);

                    return new EmployeesSalaryDetailsDto
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Age = age,
                        TyELPercent = tyelPercent,
                        UnemploymentInsurance = unemploymentInsurance,
                        TaxPercentage = e.TaxPercentage,
                        SalaryAmount = e.SalaryAmount
                    };
                }).ToList();

                return Ok(result);

            }

            catch (Exception ex)
            {
                var error = new ErrorResponseDto
                {
                    Code = databaseConnectionError,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }








        }
        // Haetaan yksittäisen työntekijän tiedot palkanlaskentaa varten
        [HttpGet("PayrollDetailsById")]
        public async Task<ActionResult<IEnumerable<EmployeesSalaryDetailsDto>>> GetPayrollDetailsById (Guid id)
        {
            var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new ErrorResponseDto
                {
                    Code = employeesNotFound
                });
            }

            var age = EmployeeHelperFunctions.CalculateAge(employee.DateOfBirth);
            var tyelPercent = PayrollHelperFunctions.CalculateTyEL(age, DateTime.Now.Year, _context);
            var UnemploymentInsuranceRate = PayrollHelperFunctions.CalculateUnemploymentInsurace(age, DateTime.Now.Year, _context);

            var employeeDto = new EmployeesSalaryDetailsDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = age,
                TyELPercent = tyelPercent,
                UnemploymentInsurance = UnemploymentInsuranceRate,
                TaxPercentage = employee.TaxPercentage,
                SalaryAmount = employee.SalaryAmount
            };

            return Ok(employeeDto);
        }

    }
}
