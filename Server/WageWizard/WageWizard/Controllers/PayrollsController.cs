using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Repositories.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollsController : ControllerBase
    {

        private readonly IPayrollRepository _repository;

        public PayrollsController(IPayrollRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("PayrollRates")]
        public async Task<ActionResult<IEnumerable<PayrollRatesDto>>> GetPayrollRatesAsync()
        {
            var rates = await _repository.GetPayrollRatesAsync();
            return Ok(rates);
        }

        [HttpGet("SalaryStatement")]
        public async Task<ActionResult<SalaryStatementCalculationDto>> CalculateSalaryStatementAsync(Guid employeeId)
        {
            var result = await _repository.CalculateSalaryStatementAsync(employeeId);

            if (result == null)
            {
                return NotFound(new ErrorResponseDto
                {
                    Code = "backend_error_messages.employee_not_found"
                });
            }

            return Ok(result);
        }
    }
}
