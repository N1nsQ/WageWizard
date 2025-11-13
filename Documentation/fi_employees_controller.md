# Dokumentaatio - Employees Controller

## GetEmployeesSummaryAsync

**Käyttö:**

- Haetaan lyhyt yhteenveto kaikkien työntekijöiden tiedoista
- Työntekijät listataan käyttöliittymällä taulukkoon, jossa nopealla silmäyksellä voi nähdä esim. nimen, kuvan ja tittelin

```C#
[HttpGet("summary")]
public async Task<ActionResult<IEnumerable<EmployeesSummaryDto>>> GetEmployeesSummaryAsync()
{
    try
    {
        var employees = await _employeeRepository.GetEmployeesSummaryAsync();

        if (!employees.Any())
        {
            var error = new ErrorResponseDto
            {
                Code = employeesNotFound
            };
            return NotFound(error);
        }

        return Ok(employees);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"{databaseConnectionError}{ex.Message}");
    }
}
```

## GetByIdAsync

**Käyttö:**

- Haetaan yksilöivän id:n perusteella yhden työntekijän tiedot Employees-tietokannasta
- Jokaisella työntekijällä on oma profiilisivu käyttöliittymällä, jossa tiedot on esitetty

```C#
[HttpGet("id")]
public async Task<ActionResult<IEnumerable<EmployeeDetailsDto>>> GetByIdAsync(Guid id)
{
    try
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
        {
            var error = new ErrorResponseDto
            {
                Code = employeesNotFound
            };
            return NotFound(error);
        }

        return Ok(employee);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"{databaseConnectionError}{ex.Message}");
    }
}
```

## GetEmployeesSalaryPaymentDetailsAsync

- Haetaan kaikilta työntekijöistä tiedot, jotka liittyvät palkanlaskentaan
- Tätä käytetään palkkalaskelmat sivun pudotusvalikossa
  - HUOM! Korvataan myöhemmin funktiolla joka palauttaa vain nimen ja id:n!

```C#
 [HttpGet("paymentDetails")]
 public async Task<ActionResult<IEnumerable<EmployeesSalaryDetailsDto>>> GetEmployeesSalaryPaymentDetailsAsync()
 {
     try
     {
         var employees = await _employeeRepository.GetEmployeesSalaryPaymentDetailsAsync();

         if (!employees.Any())
         {
             var error = new ErrorResponseDto
             {
                 Code = employeesNotFound
             };
             return NotFound(error);
         }

         return Ok(employees);
     }

     catch (Exception ex)
     {
         var error = new ErrorResponseDto
         {
             Code = databaseConnectionError,
             Message = ex.Message
         };

         return StatusCode(StatusCodes.Status500InternalServerError, error);
     }
 }
```

## GetPayrollDetailsByIdAsync

**Käyttö:**

- Haetaan yksittäiseltä työntekijältä yksilöivän id:n perusteella sellaiset tiedot, jotka vaikuttavat palkanlaskentaan.
- Tietoja käytetään yksittäisen henkilön palkan laskennassa (esim. veroprosentti, TyEL-prosentti)

```C#
[HttpGet("PayrollDetailsById")]
public async Task<ActionResult<IEnumerable<EmployeesSalaryDetailsDto>>> GetPayrollDetailsByIdAsync(Guid id)
{
    try
    {
        var employeeDto = await _employeeRepository.GetPayrollDetailsByIdAsync(id);

        if (employeeDto == null)
        {
            return NotFound(new ErrorResponseDto
            {
                Code = employeesNotFound
            });
        }

        return Ok(employeeDto);
    }
    catch (Exception ex)
    {
        var error = new ErrorResponseDto
        {
            Code = databaseConnectionError,
            Message = ex.Message
        };

        return StatusCode(StatusCodes.Status500InternalServerError, error);
    }

}
```
