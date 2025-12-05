using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "TestUser")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;


        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetByIdAsync(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            return Ok(employee);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<IEnumerable<EmployeeLookupDto>>> GetLookupAsync()
        {
            var employees = await _employeeService.GetLookupAsync();

            return Ok(employees);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<EmployeesSummaryDto>>> GetEmployeesSummaryAsync()
        {
            var employees = await _employeeService.GetEmployeesSummaryAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] NewEmployeeRequestDto dto)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(dto);

            return Ok(createdEmployee);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDto employee)
        {
            throw new NotImplementedException();
        }
    }
}
