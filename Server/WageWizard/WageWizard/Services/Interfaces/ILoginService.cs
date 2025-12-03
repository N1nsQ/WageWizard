using WageWizard.Domain.Entities;
using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto);
        string CreateJwtToken(User user);
    }
}
