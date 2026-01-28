using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;


        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EmployeeDto>> GetByIdAsync(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            return Ok(employee);
        }

        [HttpGet("lookup")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeLookupDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EmployeeLookupDto>>> GetLookupAsync()
        {
            var employees = await _employeeService.GetLookupAsync();

            return Ok(employees);
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(IEnumerable<EmployeesSummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EmployeesSummaryDto>>> GetEmployeesSummaryAsync()
        {
            var employees = await _employeeService.GetEmployeesSummaryAsync();

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateEmployee([FromBody] NewEmployeeRequestDto dto)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(dto);

            return Ok(createdEmployee);

        }

        [HttpPut("employee")]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeRequestDto employee)
        {
            var updated = await _employeeService.UpdateEmployeeAsync(id, employee);
            return Ok(updated);
        }

        [HttpPut("admin/employee")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployeeWithAdminRights(Guid id, [FromBody] UpdateEmployeeRequestWithAdminRightsDto employee)
        {
            var updated = await _employeeService.UpdateEmployeeWithAdminRightsAsync(id, employee);
            return Ok(updated);
        }
    }
}
