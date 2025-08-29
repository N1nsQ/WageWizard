using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WageWizard.Models;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollsController(PayrollContext context) : ControllerBase
    {
        private readonly PayrollContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payroll>>> GetPayrolls()
        {
            return await _context.Payrolls.ToListAsync();
        }
    }
}
