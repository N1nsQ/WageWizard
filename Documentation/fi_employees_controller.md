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

_Päivitetty 13. marraskuuta 2025_

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

_Päivitetty 13. marraskuuta 2025_

## GetEmployeesSalaryPaymentDetailsAsync

- Haetaan kaikilta työntekijöistä tiedot, jotka liittyvät palkanlaskentaan
- Palkkalaskelmat sivun pudotusvalikosta valitaan työntekijä (nimi)
- Valitun nimen perusteella lomakkeen muut kentät täyttyvät automaattisesti EmployeesSalaryDetailsDto tiedoilla.
- Tietoja käytetään valitun työntekijän palkan laskemiseen

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

_Päivitetty 13. marraskuuta 2025_
