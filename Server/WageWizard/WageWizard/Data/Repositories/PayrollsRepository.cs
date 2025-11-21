using Microsoft.EntityFrameworkCore;
using WageWizard.Repositories;
using WageWizard.Domain.Entities;

namespace WageWizard.Data.Repositories
{
    public class PayrollsRepository : IPayrollsRepository
    {
        private readonly PayrollContext _payrollContext;

        public PayrollsRepository(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _payrollContext.Employees.FindAsync(id);
        }

        public async Task<PayrollRates?> GetRatesForYearAsync(int year)
        {
            var rates = await _payrollContext.PayrollRates
                .FirstOrDefaultAsync(r => r.Year == year);

            return rates;
        }

    }
}
