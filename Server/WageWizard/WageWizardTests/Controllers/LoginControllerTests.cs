using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WageWizard;
using WageWizard.Controllers;
using WageWizard.DTOs;
using WageWizard.Models;

namespace WageWizardTests.Controllers
{
    public class LoginControllerTests
    {
        private readonly PayrollContext _payrollContext;
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            // in-memory tietokannan alustus
            var options = new DbContextOptionsBuilder<PayrollContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _payrollContext = new PayrollContext(options);

            // Lisätään testikäyttäjä tietokantaan
            _payrollContext.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = "TestUser",
                PasswordHash = "password123",
                RoleId = UserRole.TestUser
            });
            _payrollContext.SaveChanges();

            // Luodaan kontrolleri testiteitokannan kanssa
            _loginController = new LoginController(_payrollContext);
        }

        [Fact]
        public void Login_CorrectCredentialsReturnsOk()
        {
            // arrange
            var loginDto = new LoginRequestDto // tämä saadaan käyttäjältä
            {
                Username = "TestUser",
                Password = "password123"
            };

            // act
            var result = _loginController.Login(loginDto); // kutsutaan Login-metodia

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<LoginResponseDto>(okResult.Value); // okResult.Value viittaa controllerin palauttamaan LoginResponseDto -olioon

            Assert.Equal("Login successful", data.Message); 
            Assert.Equal("TestUser", data.Username);
            Assert.Equal(UserRole.TestUser, data.Role);

        }

        [Theory]
        [InlineData("Hacker", "password123")]
        [InlineData("TestUser", "password")]
        [InlineData("", "password123")]
        [InlineData("TestUser", "")]
        [InlineData("", "")]
        public void Login_IncorrectCredentialsReturnsUnauthorized(string username, string password)
        {
            // Arrange
            var loginDto = new LoginRequestDto
            {
                Username = username,
                Password = password
            };

            // Act
            var result = _loginController.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var error = Assert.IsType<ErrorResponseDto>(unauthorizedResult.Value);

            Assert.Equal("backend_error_messages.invalid_username", error.Code);

        }
    }
}
