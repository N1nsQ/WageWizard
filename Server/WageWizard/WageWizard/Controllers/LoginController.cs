using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginRequestDto>> Login([FromBody] LoginRequestDto loginDto)
        {
            var response = await _loginService.LoginAsync(loginDto);
            return Ok(response);
        }
    }
}
