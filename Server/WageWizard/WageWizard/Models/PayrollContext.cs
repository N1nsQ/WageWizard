using Microsoft.EntityFrameworkCore;

namespace WageWizard.Models
{
    public class PayrollContext : DbContext
    { 
        public PayrollContext(DbContextOptions<PayrollContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // set precision to all columns marked as decimal
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

    }
}
