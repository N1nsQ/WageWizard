using WageWizard.Domain.Entities;

namespace WageWizard.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password); // Testattu ja tarkistettu
    }
}
