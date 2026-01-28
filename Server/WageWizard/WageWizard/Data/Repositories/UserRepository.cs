using Microsoft.EntityFrameworkCore;
using WageWizard.Domain.Entities;
using WageWizard.Repositories;

namespace WageWizard.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PayrollContext _context;

        public UserRepository(PayrollContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
