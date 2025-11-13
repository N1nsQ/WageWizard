# Employee Repository - Dokumentaatio

_Repository sisältää sovelluksen **datalogiikan.** Liiketoimintalogiikka löytyy **Services** -kansiosta._

## GetEmployeesSummary

**Käyttö:**

- Työntekijät sivun taulukko näyttää pienen tiivistelmän kaikista työntekijöistä.

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

_Päivitetty 13. marraskuuta 2025_

## GetByIdAsync

**Käyttö**

- Työntekijän profiilisivulla näytetään työntekijän tarkemmat tiedot

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

_Päivitetty 13. marraskuuta 2025_

## GetEmployeesSalaryPaymentDetailsAsync

**Käyttö**

- Palkkalaskelmat sivu. Dropdown valikko näyttää kaikkien työntekijöiden nimet. Valitun nimen perusteella täytetään lomakkeen muut tiedot ja tehdään palkkalaskelma

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

_Päivitetty 13. marraskuuta 2025_
