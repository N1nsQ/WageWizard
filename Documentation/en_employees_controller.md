# Documentation - Employees Controller

## GetEmployeesSummaryAsync

**Usage:**

- Retrieves a brief summary of all employees’ information.
- Employees are displayed in a table in the user interface, allowing a quick overview of, for example, their name, photo, and job title.

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

**Usage:**

- Retrieves a single employee’s information from the Employees database based on a unique ID.
- Each employee has their own profile page in the user interface, where their details are displayed.

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

- Retrieves payroll-related information from all employees
- This is used in Salary Statements page in dropdown menu, when selecting an employee
  - HUOM! Should replace with a function that returns only name and ID

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

- Retrieves payroll-related information for a single employee based on their unique ID.
- The data is used for calculating the employee’s individual salary (e.g., tax rate, TyEL percentage).

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
