using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;


        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a specific employee by ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>Employee details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EmployeeDto>> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting employee with ID: {EmployeeId}", id);
            var employee = await _employeeService.GetByIdAsync(id);

            return Ok(employee);
        }

        /// <summary>
        /// Gets a simplified list of all employees for dropdown purposes
        /// </summary>
        /// <returns>List of employee lookup data</returns>
        [HttpGet("lookup")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeLookupDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EmployeeLookupDto>>> GetLookupAsync()
        {
            _logger.LogInformation("Getting employee lookup list");
            var employees = await _employeeService.GetLookupAsync();

            return Ok(employees);
        }

        /// <summary>
        /// Returns essential employee information including full name, job title, and email.
        /// Each employee in the list can be clicked to view detailed information via the GetByIdAsync endpoint.
        /// </summary>
        /// <returns>A list of employee summaries containing ID, name, job title, email, and profile image</returns>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(IEnumerable<EmployeesSummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EmployeesSummaryDto>>> GetEmployeesSummaryAsync()
        {
            _logger.LogInformation("Getting employee summary list");
            var employees = await _employeeService.GetEmployeesSummaryAsync();

            return Ok(employees);
        }

        /// <summary>
        /// Creates a new employee
        /// </summary>
        /// <param name="dto">Employee creation data</param>
        /// <returns>The created employee</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] NewEmployeeRequestDto dto)
        {
            _logger.LogInformation("Creating new employee: {FirstName} {LastName}", dto.FirstName, dto.LastName);
            var createdEmployee = await _employeeService.CreateEmployeeAsync(dto);

            return CreatedAtAction(
               nameof(GetByIdAsync),
               new { id = createdEmployee.Id },
               createdEmployee);

        }

        /// <summary>
        /// Updates employee information (employee role only)
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <param name="dto">Updated employee data</param>
        /// <returns>The updated employee</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Employee,TestUser")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeRequestDto employee)
        {
            _logger.LogInformation("Employee updating their own data: {EmployeeId}", id);
            var updated = await _employeeService.UpdateEmployeeAsync(id, employee);

            return Ok(updated);
        }

        /// <summary>
        /// Updates employee information with admin privileges
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <param name="dto">Updated employee data</param>
        /// <returns>The updated employee</returns>
        [HttpPut("admin/{id}")]
        [Authorize(Roles = "Admin,TestUser")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeWithAdminRights(Guid id, [FromBody] UpdateEmployeeRequestWithAdminRightsDto employee)
        {
            _logger.LogInformation("Admin updating employee data: {EmployeeId}", id);
            var updated = await _employeeService.UpdateEmployeeWithAdminRightsAsync(id, employee);

            return Ok(updated);
        }
    }
}
