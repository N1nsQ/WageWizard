namespace WageWizard.DTOs
{
    public record ErrorResponseDto
    {
        public string Code { get; init; } = string.Empty;
        public string? Message { get; init; }
    }
}
