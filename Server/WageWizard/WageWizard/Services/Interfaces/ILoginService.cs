using WageWizard.DTOs;

namespace WageWizard.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto); // Testattu ja tarkistettu
    }
}
