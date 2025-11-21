using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WageWizard.DTOs
{
    public record NewEmployeeRequestDto 
    {
        [Required][StringLength(50, MinimumLength = 2)] 
        public string FirstName { get; init; } = string.Empty;

        [Required][StringLength(50, MinimumLength = 2)] 
        public string LastName { get; init; } = string.Empty;

        [Required][StringLength(100)] 
        public string JobTitle { get; init; } = string.Empty;

        [Required][EmailAddress][StringLength(100)] 
        public string Email { get; init; } = string.Empty;

        [Required][StringLength(200)] 
        public string HomeAddress { get; init; } = string.Empty;

        [Required][RegularExpression(@"^\d{5}$", ErrorMessage = "Postal code must be 5 digits.")] 
        public string PostalCode { get; init; } = string.Empty;

        [Required][StringLength(50)] 
        public string City { get; init; } = string.Empty;

        [Required] 
        public string BankAccountNumber { get; init; } = string.Empty;

        [JsonRequired][Range(0, 100)] 
        public decimal TaxRate { get; init; }

        [Required][Range(1, double.MaxValue, ErrorMessage = "Monthly salary must be greater than 0.")] 
        public decimal MonthlySalary { get; init; }

        [Required][DataType(DataType.Date)] 
        public DateTime? StartDate { get; init; }

        [Required][DataType(DataType.Date)] 
        public DateTime? DateOfBirth { get; init; }
    };
}
