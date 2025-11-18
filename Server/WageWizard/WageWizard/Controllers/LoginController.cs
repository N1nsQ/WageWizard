using Microsoft.AspNetCore.Mvc;
using WageWizard.Data;
using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(PayrollContext context) : ControllerBase
    {
        private readonly PayrollContext _context = context;

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
