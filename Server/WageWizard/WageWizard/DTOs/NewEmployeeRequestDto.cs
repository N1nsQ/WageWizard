using System.ComponentModel.DataAnnotations;

namespace WageWizard.DTOs
{
    public record NewEmployeeRequestDto(
        [Required][StringLength(50, MinimumLength = 2)] string FirstName,
        [Required][StringLength(50, MinimumLength = 2)] string LastName,
        [Required][StringLength(100)] string JobTitle,
        [Required][EmailAddress][StringLength(100)] string Email,
        [Required][StringLength(200)] string HomeAddress,       
        [Required][RegularExpression(@"^\d{5}$", ErrorMessage = "Postal code must be 5 digits.")] string PostalCode,
        [Required][StringLength(50)] string City,
        [Required] string BankAccountNumber,
        [Required][Range(0, 100, ErrorMessage = "Tax rate must be between 0 and 100.")] decimal TaxRate,
        [Required][Range(1, double.MaxValue, ErrorMessage = "Monthly salary must be greater than 0.")] decimal MonthlySalary,
        [Required][DataType(DataType.Date)] DateTime StartDate,
        [Required][DataType(DataType.Date)] DateTime DateOfBirth
        );
}
