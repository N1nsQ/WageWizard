using Moq;
using WageWizard.Domain.Entities;
using WageWizard.Domain.Exceptions;
using WageWizard.DTOs;
using WageWizard.Repositories;
using WageWizard.Services;
using WageWizard.Services.Interfaces;

namespace WageWizardTests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly EmployeeService _employeeServiceMock;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeServiceMock = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEmployeeExists_ReturnsEmployee()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "Maija",
                LastName = "Mehiläinen",
                DateOfBirth = DateTime.Today.AddYears(-30),
                JobTitle = "testaaja",
                ImageUrl = null,
                Email = "maija.mehilainen@testi.fi",
                HomeAddress = "Pörrökuja 666",
                PostalCode = "12345",
                City = "Mehiläispesä",
                BankAccountNumber = "FI1234567890123456",
                TaxRate = 12.6m,
                GrossSalary = 3500.50m,
                StartDate = DateTime.Today,
            };


            _employeeRepositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(employee);

            // Act
            var result = await _employeeServiceMock.GetByIdAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeId, result.Id);
            Assert.Equal("Maija", result.FirstName);
        }

        [Fact]
        public async Task GetByIdAsync_WhenEmployeeDoesNotExist_ReturnsNull()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            _employeeRepositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync((Employee?)null);

            // Act
            var result = await _employeeServiceMock.GetByIdAsync(employeeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetLookupAsync_WhenEmployeesExist_ReturnsMappedList()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alice",
                    LastName = "Wonderland"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bob",
                    LastName = "Builder"
                }
            };



            _employeeRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _employeeServiceMock.GetLookupAsync();
            var list = result.ToList();

            // Assert
            Assert.Equal(2, list.Count);

            Assert.Equal($"{employees[0].FirstName} {employees[0].LastName}", list[0].FullName);
            Assert.Equal(employees[0].Id, list[0].Id);

            Assert.Equal($"{employees[1].FirstName} {employees[1].LastName}", list[1].FullName);
            Assert.Equal(employees[1].Id, list[1].Id);

            _employeeRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetEmployeesSummaryAsync_WhenEmployeesExist_ReturnsMappedList()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jack",
                    LastName = "Sparrow",
                    JobTitle = "Captain",
                    Email = "jack.sparrow@test.com",
                    ImageUrl = null
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jones",
                    LastName = "Sparrow",
                    JobTitle = "Villain",
                    Email = "jones.sparrow@test.com",
                    ImageUrl = null
                }
            };

            _employeeRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _employeeServiceMock.GetEmployeesSummaryAsync();
            var list = result.ToList();

            // Assert
            Assert.Equal(2, list.Count);

            Assert.Equal("Jack", list[0].FirstName);
            Assert.Equal("Sparrow", list[0].LastName);
            Assert.Equal("Captain", list[0].JobTitle);
            Assert.Equal("jack.sparrow@test.com", list[0].Email);

            Assert.Equal("Jones", list[1].FirstName);
            Assert.Equal("Sparrow", list[1].LastName);
            Assert.Equal("Villain", list[1].JobTitle);
            Assert.Equal("jones.sparrow@test.com", list[1].Email);

            _employeeRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ValidDto_CreatesEmployee()
        {
            // Arrange
            var dto = new NewEmployeeRequestDto
            {
                FirstName = "Maija",
                LastName = "Virtanen",
                JobTitle = "Developer",
                Email = "maija.virtanen@example.com",
                HomeAddress = "Katu 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI1234567890123456",
                TaxRate = 20,
                MonthlySalary = 3000,
                StartDate = DateTime.Today,
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            _employeeRepositoryMock
                .Setup(r => r.FindDuplicateAsync(dto.FirstName, dto.LastName, dto.Email))
                .ReturnsAsync((Employee?)null);

            Employee? addedEmployee = null;
            _employeeRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .Callback<Employee>(e => addedEmployee = e)
                .ReturnsAsync((Employee e) => e);

            var service = new EmployeeService(_employeeRepositoryMock.Object);

            // Act
            var result = await service.CreateEmployeeAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.FirstName, result.FirstName);
            Assert.Equal(dto.LastName, result.LastName);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.MonthlySalary, result.GrossSalary);
            Assert.Equal(dto.StartDate.Value, result.StartDate);
            Assert.Equal(dto.DateOfBirth.Value, result.DateOfBirth);

            _employeeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);

            Assert.NotNull(addedEmployee);
            Assert.Equal(dto.FirstName, addedEmployee.FirstName);
            Assert.Equal(dto.LastName, addedEmployee.LastName);
            Assert.Equal(dto.Email, addedEmployee.Email);
            Assert.Equal(dto.MonthlySalary, addedEmployee.GrossSalary);
            Assert.Equal(dto.StartDate.Value, addedEmployee.StartDate);
            Assert.Equal(dto.DateOfBirth.Value, addedEmployee.DateOfBirth);
        }

        [Fact]
        public async Task CreateEmployeeAsync_DuplicateEmployee_ThrowsDuplicateEmployeeException()
        {
            // Arrange
            var dto = new NewEmployeeRequestDto
            {
                FirstName = "Maija",
                LastName = "Virtanen",
                Email = "maija.virtanen@example.com",
                DateOfBirth = DateTime.Today.AddYears(-30),
                StartDate = DateTime.Today
            };

            var existingEmployee = new Employee { Id = Guid.NewGuid() };
            _employeeRepositoryMock
                .Setup(r => r.FindDuplicateAsync(dto.FirstName, dto.LastName, dto.Email))
                .ReturnsAsync(existingEmployee);

            // Act
            Func<Task> act = () => _employeeServiceMock.CreateEmployeeAsync(dto);

            // Assert
            var exception = await Assert.ThrowsAsync<DuplicateEmployeeException>(act);
            Assert.Equal("Employee with identical details already exists.", exception.Message);

            _employeeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldUpdateOnlyProvidedFields()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            var existingEmployee = new Employee
            {
                Id = employeeId,
                FirstName = "Maija",
                LastName = "Mehiläinen",
                HomeAddress = "Vanha osoite 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI11",
                TaxRate = 20,
                GrossSalary = 3000,
                StartDate = DateTime.Today.AddYears(-1),
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            _employeeRepositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(existingEmployee);

            var dto = new UpdateEmployeeRequestDto("Uusi katu 123", null, "Espoo", null);

            Employee? updatedEmployee = null;

            _employeeRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Employee>()))
                .Callback<Employee>(e => updatedEmployee = e)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _employeeServiceMock.UpdateEmployeeAsync(employeeId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Uusi katu 123", result.HomeAddress);
            Assert.Equal("Espoo", result.City);
            Assert.Equal("00100", result.PostalCode);

            _employeeRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Once);
        }
        [Fact]
        public async Task UpdateEmployeeWithAdminRightsAsync_ShouldUpdateOnlyProvidedFields()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

            var existingEmployee = new Employee
            {
                Id = employeeId,
                FirstName = "Maija",
                LastName = "Mehiläinen",
                JobTitle = "Pörriäinen",
                HomeAddress = "Vanha osoite 1",
                PostalCode = "00100",
                City = "Helsinki",
                BankAccountNumber = "FI11",
                TaxRate = 20,
                GrossSalary = 3000,
                StartDate = DateTime.Today.AddYears(-1),
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            _employeeRepositoryMock
                .Setup(r => r.GetByIdAsync(employeeId))
                .ReturnsAsync(existingEmployee);

            var dto = new UpdateEmployeeRequestWithAdminRightsDto
                (
                null,
                "Kimalainen",
                null,
                "Senior Pörriäinen",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
                );

            Employee? updatedEmployee = null;

            _employeeRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Employee>()))
                .Callback<Employee>(e => updatedEmployee = e)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _employeeServiceMock.UpdateEmployeeWithAdminRightsAsync(employeeId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Kimalainen", result.LastName);
            Assert.Equal("Senior Pörriäinen", result.JobTitle);
            Assert.Equal("Maija", result.FirstName);
            Assert.Equal("Vanha osoite 1", result.HomeAddress);

            _employeeRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Once);
        }

    }
}
