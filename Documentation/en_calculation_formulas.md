# Calculation Formulas | English Documentation

## Calculate TyEL Percentage

The employee pension contribution (TyEL) is determined based on the employee's age and the calendar year. The percentages may change annually and are retrieved from the database (`PayrollRates` table).

- Possible TyEL percentage options:

  1. **Zero percent** – employees under 17 years old and over 67 years old
  2. **Basic rate** – employees aged 17–52 and 63–67
  3. **Higher rate (Senior)** – employees aged 53–62, 8.65 %

- Values are stored in the `PayrollRates` table in the database and maintained manually.

  ```C#
        public static decimal GetTyELPercent(int age, int year, PayrollContext context)
        {
            if (age < 17 || age > 67)
                return 0m;

            var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year)
                        ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

            if (age >= 53 && age <= 62)
                return rates.TyEL_Senior;

            return rates.TyEL_Basic;
        }
  ```

The function takes the following parameters:

- `int age` – the employee's age
- `int year` – the current year
- `PayrollContext context` – Entity Framework database context containing payroll-related tables such as `PayrollRates`

Function behavior:

- If the employee's age is under 17 or over 67, the function returns 0 %.
- The TyEL percentages for the specified year are retrieved from the database (`PayrollRates` table):
  - A record with `Year = year` is fetched.
  - If the employee is aged between 53 and 62 (inclusive), the Senior rate is returned.
  - If no record is found, the function throws a `KeyNotFoundException`.
  - Otherwise, the basic rate is returned.

### Example

| Age | Year | Expected TyEL Percentage (%) | Description    |
| --- | ---- | ---------------------------- | -------------- |
| 16  | 2025 | 0 %                          | Under 17 years |
| 25  | 2025 | 7.15 %                       | Basic rate     |
| 55  | 2025 | 8.65 %                       | Senior rate    |
| 68  | 2025 | 0 %                          | Over 67 years  |

## Calculate Unemployment Insurance Percentage

The employee's salary is subject to an unemployment insurance contribution if the employee is 18 years old or older and under 65. The percentage may change annually, and the current rate is retrieved from the database (`PayrollRates` table). The database is maintained manually.

```C#
public static decimal GetUnemploymentInsurancePercent(int age, int year, PayrollContext context)
{
    if (age < 18 || age >= 65)
        return 0m;

    var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year)
                ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

    return rates.UnemploymentInsurance;
}
```

The function takes the following parameters:

- `int age` – the employee's age
- `int year` – the current year
- `PayrollContext context` – Entity Framework database context containing payroll-related tables such as `PayrollRates`

Function behavior:

- If the employee's age is under 18 or 65 or older, the function returns 0 %.
- Otherwise, the unemployment insurance percentage for the specified year is retrieved from the database.

### Example

| Age | Year | Expected UI Percentage (%) | Description       |
| --- | ---- | -------------------------- | ----------------- |
| 16  | 2025 | 0 %                        | Under 18 years    |
| 25  | 2025 | 0.59 %                     | UI rate 2025      |
| 55  | 2020 | 1.25 %                     | UI rate 2020      |
| 68  | 2025 | 0 %                        | 65 years or older |

\*UI = Unemployment Insurance
