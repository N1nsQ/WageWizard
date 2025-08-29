using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Models;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(PayrollContext context) : ControllerBase
    {
        private readonly PayrollContext _context = context;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.PasswordHash == loginDto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                username = user.Username,
                role = user.RoleId
            });
        }

        
    }
}
