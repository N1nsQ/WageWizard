using System.ComponentModel.DataAnnotations;

namespace WageWizard.Models
{
    public sealed class Role
    {
        [Key]
        public UserRole RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
