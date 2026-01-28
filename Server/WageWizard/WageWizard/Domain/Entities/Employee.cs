namespace WageWizard.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal TaxRate { get; set; }
        public decimal GrossSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
