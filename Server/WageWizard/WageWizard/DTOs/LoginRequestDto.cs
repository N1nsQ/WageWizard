namespace WageWizard.DTOs
{
    // Testattu ja tarkistettu
    public record LoginRequestDto
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
