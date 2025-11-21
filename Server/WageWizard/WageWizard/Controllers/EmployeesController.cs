using Microsoft.AspNetCore.Mvc;
using WageWizard.Domain.Entities;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;


        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetByIdAsync(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            return Ok(employee);
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
            _ = await _employeeService.CreateEmployeeAsync(dto);
            return Ok();
        }
    }
}
