using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WageWizard.Controllers;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Services.Interfaces;

namespace WageWizardTests.Controllers
{
    public class LoginControllerTests
    {
        private readonly Mock<ILoginService> _loginServiceMock;
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            _loginServiceMock = new Mock<ILoginService>();
            _loginController = new LoginController(_loginServiceMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = "Maija",
                Password = "Mehiläinen"
            };

            var response = new LoginResponseDto
            {
                Success = true,
                Message = "Login successful",
                UserId = Guid.NewGuid(),
                Username = "Maija",
                Role = UserRole.TestUser
            };

            _loginServiceMock.Setup(s => s.LoginAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _loginController.Login(request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(response);
        }


        [Theory]
        [InlineData("Hacker", "password123")]
        [InlineData("TestUser", "password")]
        [InlineData("", "password123")]
        [InlineData("TestUser", "")]
        [InlineData("", "")]
        public async Task Login_ShouldThrowUnauthorizedException_WhenCredentialsAreInvalid(string username, string password)
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = username,
                Password = password
            };

            _loginServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginRequestDto>(
                    x => x.Username == username && x.Password == password)))
                .ThrowsAsync(new UnauthorizedException("Invalid username or password."));

            // Act
            Func<Task> act = () => _loginController.Login(request);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedException>()
                 .WithMessage("Invalid username or password.");

        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = "user",
                Password = "pass"
            };

            _loginServiceMock
                .Setup(s => s.LoginAsync(It.IsAny<LoginRequestDto>()))
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            Func<Task> act = async () => await _loginController.Login(request);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Something went wrong");
        }
    }
}
