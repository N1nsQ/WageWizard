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
                return Unauthorized(new ErrorResponseDto 
                { 
                    Code = "backend_error_messages.invalid_username" 
                });
            }

            return Ok(new LoginResponseDto
            {
                Success = true,
                Message = "Login successful",
                UserId = user.Id,
                Username = loginDto.Username,
                Role = user.RoleId ?? UserRole.TestUser
            });
        }
    }
}
