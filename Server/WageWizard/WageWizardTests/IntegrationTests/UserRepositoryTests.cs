using Microsoft.EntityFrameworkCore;
using WageWizard.Data.Repositories;
using WageWizard.Data;
using WageWizard.Domain.Entities;

namespace WageWizardTests.IntegrationTests
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<PayrollContext> _dbOptions;
        public UserRepositoryTests()
        {
            _dbOptions = new DbContextOptionsBuilder<PayrollContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
        }

        [Fact]
        public async Task GetUserByUsernameAndPasswordAsync_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Username = "Maija", Password = "Salasana123" };
            await using var context = new PayrollContext(_dbOptions);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetUserByUsernameAndPasswordAsync("Maija", "Salasana123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result!.Id);
        }

        [Fact]
        public async Task GetUserByUsernameAndPasswordAsync_ShouldReturnNull_WhenCredentialsAreIncorrect()
        {
            // Arrange
            await using var context = new PayrollContext(_dbOptions);
            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetUserByUsernameAndPasswordAsync("Hacker", "wrongpass");

            // Assert
            Assert.Null(result);
        }
    }


}
