using System.ComponentModel.DataAnnotations;

namespace WageWizard.DTOs
{
    public record LoginRequestDto
    {
        [Required(ErrorMessage = "Username is required")] public string Username { get; init; } = string.Empty;
        [Required(ErrorMessage = "Password is required")] public string Password { get; init; } = string.Empty;
    }
}
