using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WageWizard.Models;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/Payrolls")]
    public class PayrollsController : ControllerBase
    {
        private readonly PayrollContext _context;

        public PayrollsController(PayrollContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payroll>>> GetPayrolls()
        {
            return await _context.Payrolls.ToListAsync();
        }
    }
}
