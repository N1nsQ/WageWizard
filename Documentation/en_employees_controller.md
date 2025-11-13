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

_Updated Nov 13, 2025_

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

_Updated Nov 13, 2025_

## GetEmployeesSalaryPaymentDetailsAsync

- Retrieves payroll-related information from all employees
- This is used in Salary Statements page in dropdown menu, when selecting an employee
- Other information on the form is updated based on EmployeesSalaryDetailsDto data
- The data is also used for calculating the employee’s individual salary

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

_Updated Nov 13, 2025_
