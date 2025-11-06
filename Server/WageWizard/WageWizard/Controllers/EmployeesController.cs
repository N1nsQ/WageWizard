using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WageWizard.DTOs;
using WageWizard.Models;
using WageWizard.Repositories;
using WageWizard.Utils;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        //private readonly PayrollContext _context = context;
        private readonly IEmployeeRepository _employeeRepository;

        const string employeesNotFound = "backend_error_messages.employees_not_found";
        const string databaseConnectionError = "Database connection error: ";

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Get all information from all of the employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return Ok(employees);
        }

        // Get some basic information from all employees
        // These details are shown in the summary-table in frontend
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<EmployeesSummaryDto>>> GetEmployeesSummaryAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesSummaryAsync();

                if (!employees.Any())
                {
                    var error = new ErrorResponseDto
                    {
                        Code = employeesNotFound
                    };
                    return NotFound(error);
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{databaseConnectionError}{ex.Message}");
            }
        }

        //// EmployeeDetails page
        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<EmployeeDetailsDto>>> GetByIdAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);

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
            catch (Exception ex)
            {
                return StatusCode(500, $"{databaseConnectionError}{ex.Message}");
            }
        }

        //// Haentaan kaikkien työntekijöiden tiedot palkanlaskentaa varten
        [HttpGet("paymentDetails")]
        public async Task<ActionResult<IEnumerable<EmployeesSalaryDetailsDto>>> GetEmployeesSalaryPaymentDetailsAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesSalaryPaymentDetailsAsync();

                if (!employees.Any())
                {
                    var error = new ErrorResponseDto
                    {
                        Code = employeesNotFound
                    };
                    return NotFound(error);
                }

                return Ok(employees);
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

        //// Haetaan yksittäisen työntekijän tiedot palkanlaskentaa varten
        [HttpGet("PayrollDetailsById")]
        public async Task<ActionResult<IEnumerable<EmployeesSalaryDetailsDto>>> GetPayrollDetailsByIdAsync(Guid id)
        {
            try
            {
                var employeeDto = await _employeeRepository.GetPayrollDetailsByIdAsync(id);

                if (employeeDto == null)
                {
                    return NotFound(new ErrorResponseDto
                    {
                        Code = employeesNotFound
                    });
                }

                return Ok(employeeDto);
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
    }
}
