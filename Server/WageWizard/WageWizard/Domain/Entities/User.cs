namespace WageWizard.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; } 
        public string Password { get; set; } = string.Empty;
        public UserRole? RoleId { get; set; }
        public bool? IsActive { get; set; }

    }
}
