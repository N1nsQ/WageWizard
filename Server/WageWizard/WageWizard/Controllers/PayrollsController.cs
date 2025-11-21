using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollsController : ControllerBase
    {

        private readonly IPayrollsService _payrollsService;

        public PayrollsController(IPayrollsService payrollsService)
        {
            _payrollsService = payrollsService;
        }

        [HttpGet("salaryStatement/{employeeId}")]
        public async Task<ActionResult<SalaryStatementCalculationDto>> CalculateSalaryStatementAsync(Guid employeeId)
        {
            var result = await _payrollsService.CalculateSalaryStatementAsync(employeeId);

            return Ok(result);
        }
    }
}
