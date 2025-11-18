using Microsoft.AspNetCore.Mvc;
using WageWizard.Domain.Entities;
using WageWizard.DTOs;
using WageWizard.Repositories.Interfaces;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        const string employeesNotFound = "backend_error_messages.employees_not_found";
        const string databaseConnectionError = "Database connection error: ";

        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;

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

        [HttpGet("id/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeDetailsDto>>> GetEmpyeeByIdAsync(Guid id)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetByIdAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee == null)
                {
                    return NotFound(new ErrorResponseDto { Code = employeesNotFound });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{databaseConnectionError}{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] NewEmployeeRequestDto dto)
        {
            var employee = await _employeeService.CreateEmployeeAsync(dto);
            return Ok();
        }
    }
}
