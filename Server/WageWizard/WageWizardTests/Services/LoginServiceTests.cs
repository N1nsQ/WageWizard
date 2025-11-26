using Microsoft.AspNetCore.Identity.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loginService = new LoginService( _userRepositoryMock.Object );
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnOkResponse_WhenCredentiealsAreValid()
        {
            // Arrange
            var loginDto = new LoginRequestDto
            {
                Username = "Maija",
                Password = "Salasana123",
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Maija",
                RoleId = UserRole.TestUser
            };

            _userRepositoryMock
                .Setup(r => r.GetUserByUsernameAndPasswordAsync(loginDto.Username, loginDto.Password))
                .ReturnsAsync(user);

            // Act
            var result = await _loginService.LoginAsync(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Login Successful", result.Message);
            Assert.Equal(user.Id, result.UserId);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(UserRole.TestUser, result.Role);
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

            var service = new LoginService(repoMock.Object);

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
