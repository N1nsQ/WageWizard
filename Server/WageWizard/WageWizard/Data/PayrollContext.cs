using Microsoft.EntityFrameworkCore;
using WageWizard.Domain.Entities;

namespace WageWizard.Data
{
    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PayrollRates> PayrollRates { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // set precision to all columns marked as decimal
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(r => new Role
                {
                    RoleId = r,
                    RoleName = r.ToString(),
                }));
        }

    }
}
