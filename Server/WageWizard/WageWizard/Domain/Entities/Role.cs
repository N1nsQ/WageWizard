using System.ComponentModel.DataAnnotations;

namespace WageWizard.Domain.Entities
{
    public sealed class Role
    {
        [Key]
        public UserRole RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
