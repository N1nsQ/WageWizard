# Employee Repository - Documentation

_The **Repository** contains the application's **data logic**. The **business logic** is located in the **Services** folder._

## GetEmployeesSummary

**Usage:**

- The Employees page table displays a brief summary of all employees.

```C#
 public async Task<IEnumerable<EmployeesSummaryDto>> GetEmployeesSummaryAsync()
 {
     return await _payrollContext.Employees
         .OrderBy(e => e.LastName)
         .Select(e => new EmployeesSummaryDto
         (
             e.Id,
             e.FirstName,
             e.LastName,
             e.JobTitle,
             e.ImageUrl,
             e.Email
         ))
         .ToListAsync();
 }
```

_Updated Nov 13, 2025_

## GetByIdAsync

**Usage**

- The employee profile page displays detailed information about the employee.

```C#
public async Task<EmployeeDetailsDto?> GetByIdAsync(Guid id)
{
   return await _payrollContext.Employees
       .Where(e => e.Id == id)
       .Select(e => new EmployeeDetailsDto
       (
           e.Id,
           e.FirstName,
           e.LastName,
           EmployeeHelperFunctions.CalculateAge(e.DateOfBirth),
           e.JobTitle,
           e.ImageUrl,
           e.Email,
           e.HomeAddress,
           e.PostalCode,
           e.City,
           e.BankAccountNumber,
           e.TaxPercentage,
           e.SalaryAmount,
           e.StartDate
       ))
       .FirstOrDefaultAsync();
}
```

_Updated Nov 13, 2025_

## GetEmployeesSalaryPaymentDetailsAsync

\*_Usage_

- Salary statement page. A dropdown menu displays the names of all employees. Based on the selected name, the rest of the form is filled out, and a payroll calculation is performed.

```C#
 public async Task<EmployeesSalaryDetailsDto?> GetPayrollDetailsByIdAsync(Guid id)
 {
     var employee = await _payrollContext.Employees
         .FirstOrDefaultAsync(e => e.Id == id);

     if (employee == null)
         return null;

     var age = EmployeeHelperFunctions.CalculateAge(employee.DateOfBirth);
     var tyelPercent = PayrollHelperFunctions.GetTyELPercent(age, DateTime.Now.Year, _payrollContext);
     var unemploymentInsurance = PayrollHelperFunctions.GetUnemploymentInsurancePercent(age, DateTime.Now.Year, _payrollContext);

     return new EmployeesSalaryDetailsDto
     (
         employee.Id,
         employee.FirstName,
         employee.LastName,
         age,
         tyelPercent,
         unemploymentInsurance,
         employee.TaxPercentage,
         employee.SalaryAmount
     );
 }
```

_Updated Nov 13, 2025_
