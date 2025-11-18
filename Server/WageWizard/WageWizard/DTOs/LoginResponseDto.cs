using WageWizard.Domain.Entities;

namespace WageWizard.DTOs
{
    public record LoginResponseDto
    {
        public bool Success { get; init; }
        public string? Token { get; init; }
        public string? Message { get; init; }
        public Guid UserId { get; init; }
        public string Username { get; init; } = string.Empty;
        public UserRole Role { get; init; }
    }
}
