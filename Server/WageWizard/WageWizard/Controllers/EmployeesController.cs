using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WageWizard.DTOs;
using WageWizard.Models;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(PayrollContext context) : ControllerBase
    {
        private readonly PayrollContext _context = context;

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<EmployeesListDto>>> GetEmployeesSummary()
        {
            

            var employees = await _context.Employees
                .OrderBy(e => e.LastName)
                .Select(e => new EmployeesListDto
                {
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
                    Code = "backend_error_messages.employees_not_found"
                };

            return NotFound(error);

            }

            return Ok(employees);
        }

    }
}
