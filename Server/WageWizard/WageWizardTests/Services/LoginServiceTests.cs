using Microsoft.Extensions.Configuration;
using Moq;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Repositories;
using WageWizard.Services;

namespace WageWizardTests.Services
{
    public class LoginServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IConfiguration _configuration;
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var inMemorySettings = new Dictionary<string, string?> {
            {"Jwt:Key", "supersecretkey_supersecretkey"},
            {"Jwt:Issuer", "TestIssuer"},
            {"Jwt:Audience", "TestAudience"},
        };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _loginService = new LoginService(_userRepositoryMock.Object, _configuration);
        }


        [Theory]
        [InlineData("Maija", "WrongPassword")]
        [InlineData("Hacker", "1234")]
        public async Task LoginAsync_ShouldThrowUnauthorizedException_WhenCredentialsAreInvalid(string username, string password)
        {
            // Arrange
            var loginDto = new LoginRequestDto
            {
                Username = username,
                Password = password
            };

            _userRepositoryMock
                .Setup(r => r.GetUserByUsernameAndPasswordAsync(username, password))
                .ReturnsAsync((User?)null);

            // Act
            Func<Task> act = async () => await _loginService.LoginAsync(loginDto);

            // Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedException>(act);
            Assert.Equal("Invalid username or password.", ex.Message);
        }

        [Fact]
        public async Task Login_Should_Throw_RepositoryUnavailableException_When_Repo_Fails()
        {
            var repoMock = new Mock<IUserRepository>();
            repoMock
                .Setup(r => r.GetUserByUsernameAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new RepositoryUnavailableException("Database connection failed."));

            var service = new LoginService(repoMock.Object, _configuration);

            var ex = await Assert.ThrowsAsync<RepositoryUnavailableException>(() =>
                service.LoginAsync(new LoginRequestDto
                {
                    Username = "test",
                    Password = "123"
                }));

            Assert.Equal("Database connection failed.", ex.Message);
        }
    }
}
