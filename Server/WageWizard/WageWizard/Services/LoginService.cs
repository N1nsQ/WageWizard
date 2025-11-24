using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Repositories;
using WageWizard.Services.Interfaces;

namespace WageWizard.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(
                loginDto.Username, loginDto.Password);

            if (user == null)
                throw new UnauthorizedException("Invalid username or password.");

            return new LoginResponseDto
            {
                Success = true,
                Message = "Login Successful",
                UserId = user.Id,
                Username = user.Username,
                Role = user.RoleId ?? UserRole.TestUser
            };
        }
    }
}
