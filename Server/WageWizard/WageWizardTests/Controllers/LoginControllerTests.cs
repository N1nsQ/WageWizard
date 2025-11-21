using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard;
using WageWizard.Controllers;
using WageWizard.Data;
using WageWizard.Domain.Entities;
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

        //[Fact]
        //public void Login_CorrectCredentialsReturnsOk()
        //{
        //    // arrange
        //    var loginDto = new LoginRequestDto
        //    {
        //        Username = "TestUser",
        //        Password = "password123"
        //    };

        //    // act
        //    var result = _loginController.Login(loginDto);

        //    // assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var data = Assert.IsType<LoginResponseDto>(okResult.Value); 

        //    Assert.Equal("Login successful", data.Message); 
        //    Assert.Equal("TestUser", data.Username);
        //    Assert.Equal(UserRole.TestUser, data.Role);

        //}

        //[Theory]
        //[InlineData("Hacker", "password123")]
        //[InlineData("TestUser", "password")]
        //[InlineData("", "password123")]
        //[InlineData("TestUser", "")]
        //[InlineData("", "")]
        //public void Login_IncorrectCredentialsReturnsUnauthorized(string username, string password)
        //{
        //    // Arrange
        //    var loginDto = new LoginRequestDto
        //    {
        //        Username = username,
        //        Password = password
        //    };

        //    // Act
        //    var result = _loginController.Login(loginDto);

        //    // Assert
        //    var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        //    var error = Assert.IsType<ErrorResponseDto>(unauthorizedResult.Value);

        //    Assert.Equal("backend_error_messages.invalid_username", error.Code);

        //}
    }
}
