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
        private readonly IEmployeeRepository _employeeRepository;

        const string employeesNotFound = "backend_error_messages.employees_not_found";
        const string databaseConnectionError = "Database connection error: ";

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

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
