using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WageWizard.Config;
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
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(
                loginDto.Username, loginDto.Password);

            if (user == null)
                throw new UnauthorizedException("Invalid username or password.");

            var token = CreateJwtToken(user);

            return new LoginResponseDto
            {
                Success = true,
                Message = "Login Successful",
                Token = token,
                UserId = user.Id,
                Username = user.Username,
                Role = user.RoleId ?? UserRole.TestUser
            };

        }

        public string CreateJwtToken(User user)
        {
            var jwt = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(jwt.ExpiresHours),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
