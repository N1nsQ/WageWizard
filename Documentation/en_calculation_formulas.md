# Calculation Formulas | English Documentation

## Calculate TyEL Percentage

The employee’s pension contribution (TyEL) is determined based on the employee’s age and the calendar year. Percentages can change annually and are retrieved from the `PayrollRates` table in the database. The database is maintained manually.

**TyEL percentages in 2025**

1. **Zero percent, 0 %** – employees under 17 years old and over 67 years old
2. **Basic percentage, 7.15 %** – employees aged 17–52 and 63–67
3. **Higher percentage (Senior), 8.65 %** – employees aged 53–62, 8.65 %

```C#
// PayrollHelperFunctions.cs
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

**Parameters:**

- `int age` – the employee's age
- `int year` – the current year
- `PayrollContext context` – Database context with payroll-related tables

**Possible return values:**

- **0 %** → Employee under 17 or over 67
- **Basic percentage** → Employees 17-52 and 63-67
- **Higher percentage (Senior)** → Employees 53-62
- **KeyNotFoundException** → Thrown if data for the specified year is missing

### Example

| Age | Year | Expected TyEL Percentage (%) | Description    |
| --- | ---- | ---------------------------- | -------------- |
| 16  | 2025 | 0 %                          | Under 17 years |
| 25  | 2025 | 7.15 %                       | Basic rate     |
| 55  | 2025 | 8.65 %                       | Senior rate    |
| 68  | 2025 | 0 %                          | Over 67 years  |

_Updated Nov 11, 2025_

## Calculate Unemployment Insurance Percentage

The employee’s unemployment insurance contribution is deducted from their salary if they are at least 18 and under 65. The rate may vary yearly and is retrieved from the `PayrollRates` table.

**Employee unemployment insurance rates for different years**

- 2020 → 1.25 %
- 2023 → 0.50 %
- 2025 → 0.59 %

```C#
// PayrollHelperFunctions.cs
public static decimal GetUnemploymentInsurancePercent(int age, int year, PayrollContext context)
{
    if (age < 18 || age >= 65)
        return 0m;

    var rates = context.PayrollRates.FirstOrDefault(r => r.Year == year)
                ?? throw new KeyNotFoundException($"TyEL rates not found for year {year}");

    return rates.UnemploymentInsurance;
}
```

**Parameters:**

- `int age` – the employee's age
- `int year` – the current year
- `PayrollContext context` – Database context with payroll-related tables

**Possible return values:**

- **0 %** → Employee under 18 or 65 and older
- **UI rate for the given year** → Employee aged 18-64
- **KeyNotFoundException** → Data missing

### Example

| Age | Year | Expected UI Percentage (%) | Description       |
| --- | ---- | -------------------------- | ----------------- |
| 16  | 2025 | 0 %                        | Under 18 years    |
| 25  | 2025 | 0.59 %                     | UI rate 2025      |
| 55  | 2020 | 1.25 %                     | UI rate 2020      |
| 68  | 2025 | 0 %                        | 65 years or older |

\*UI = Unemployment Insurance

_Updated Nov 11, 2025_

## Calculate TyEL Amount

This function calculates the employee’s TyEL contribution in euros based on their gross salary and TyEL percentage.

```C#
// PayrollServices.cs

public static decimal CalculateTyELAmount(decimal grossSalary, decimal tyelPercent)
{
    decimal tyelAmount = Math.Round((grossSalary * tyelPercent), 2, MidpointRounding.AwayFromZero);

     return tyelAmount;
}
```

- **Parameters**
  - `decimal grossSalary` – Employee’s gross salary in euros
  - `decimal tyelPercent` – Calculated TyEL percentage
- **Return value:** TyEL contribution amount in euros, rounded to two decimal places
- **Rounding:** The result is rounded to two decimal places using the `MidpointRounding.AwayFromZero` rule.
  This means that if the value is exactly halfway between two possible decimals, it is rounded away from zero:
  - 0.005 € → 0.01 €
  - 0.015 € → 0.02 €

### Example

| Gross Salary (€) | TyEL % | Expected TyEL amount (€) |
| ---------------- | ------ | ------------------------ |
| 3,000.00         | 7.15 % | 214.50                   |
| 4,200.00         | 8.65 % | 363.30                   |
| 2,000.00         | 0 %    | 0.00                     |

_Updated Nov 11, 2025_

## Calculate Unemployment Insurance Amount

This function calculates the employee’s unemployment insurance contribution in euros.

```csharp
// PayrollServices.cs

public static decimal CalculateUnemploymentInsuranceAmount(decimal grossSalary, decimal unemploymentInsurancePercent)
{
    decimal unemploymentInsuranceAmount = Math.Round((grossSalary * unemploymentInsurancePercent), 2, MidpointRounding.AwayFromZero);

    return unemploymentInsuranceAmount;
}
```

- **Parameters:**
  - `decimal grossSalary` – Employee’s gross salary
  - `decimal unemploymentInsurancePercent` – Calculated UI percentage
- **Return value:** Unemployment insurance contribution in euros, rounded to two decimals
- **Rounding rule:** The result is rounded to two decimal places using the `MidpointRounding.AwayFromZero` rule.
  This means that if the value is exactly halfway between two possible decimals, it is rounded away from zero:
  - 0.005 € → 0.01 €
  - 0.015 € → 0.02 €

| Gross Salary (€) | TyEL % | Expected UI Amount (€) |
| ---------------- | ------ | ---------------------- |
| 2,500.00         | 0.59 % | 14.75                  |
| 4,000.00         | 1.25 % | 50.00                  |
| 17,000.00        | 0 %    | 0.00                   |

- \*UI = Unemployment Insurance

_Updated Nov 11, 2025_

## Calculate Withholding Tax

Withholding tax is calculated based on the employee’s tax rate. Currently only sample employees exist, no integration with the tax authority.

```C#
// PayrollServices.cs

public static decimal CalculateWithholdingTaxAmount(decimal grossSalary, decimal taxPercent)
{
    decimal withholdingTaxAmount = Math.Round((grossSalary * (taxPercent / 100)), 2, MidpointRounding.AwayFromZero);
    return withholdingTaxAmount;
}
```

- **Parameters:**
  - `decimal grossSalary` – Employee’s gross salary
  - `decimal taxPercent` – Tax rate (e.g., 11.00 = 11 %)
- **Return value:** Withholding tax amount in euros, rounded to two decimals
- **Rounding rule:** The result is rounded to two decimal places using the `MidpointRounding.AwayFromZero` rule.
  This means that if the value is exactly halfway between two possible decimals, it is rounded away from zero:
  - 0.005 € → 0.01 €
  - 0.015 € → 0.02 €

| Gross Salary (€) | Tax %   | Expected Withholding Tax (€) |
| ---------------- | ------- | ---------------------------- |
| 2,500.00         | 11.00 % | 275.00                       |
| 4,200.00         | 15.50 % | 651.00                       |
| 1,000.00         | 0 %     | 0.00                         |

_Updated Nov 11, 2025_

## Calculate Net Salary

Net salary is calculated by subtracting withholding tax, TyEL contribution, and UI from gross salary. Each deduction is calculated with its own function.

```C#
// PayrollServices.cs

public static decimal CalculateNetSalaryAmount(
    decimal grossSalary,
    decimal taxPercent,
    decimal tyelPercent,
    decimal unemploymentInsurancePercent)
{
    decimal withholdingTax = CalculateWithholdingTaxAmount(grossSalary, taxPercent);
    decimal tyel = CalculateTyELAmount(grossSalary, tyelPercent);
    decimal unemploymentInsurance = CalculateUnemploymentInsuranceAmount(grossSalary, unemploymentInsurancePercent);

    decimal netSalary = Math.Round((grossSalary - (withholdingTax + tyel + unemploymentInsurance)),2, MidpointRounding.AwayFromZero);

    return netSalary;
}
```

- **Parameters:**
  - `decimal grossSalary` – Employee’s gross salary
  - `decimal taxPercent` – Tax rate (e.g., 11.00 = 11 %)
  - `decimal tyelPercent` – Calculated TyEL %
  - `decimal unemploymentInsurancePercent` – Calculated UI %
- **Return value:** Net salary in euros, rounded to two decimals
- **Rounding:** The result is rounded to two decimal places using the `MidpointRounding.AwayFromZero` rule.
  This means that if the value is exactly halfway between two possible decimals, it is rounded away from zero:
  - 0.005 € → 0.01 €
  - 0.015 € → 0.02 €

| Gross Salary (€) | Tax % | TyEL % | UI %   | Expected Net Salary (€) |
| ---------------- | ----- | ------ | ------ | ----------------------- |
| 3,000.00         | 11.00 | 7.15 % | 0.59 % | 2,437.80                |
| 4,200.00         | 15.00 | 8.65 % | 1.25 % | 3,430.28                |
| 2,500.00         | 0.00  | 7.15 % | 0.59 % | 2,306.50                |

- \*UI = Unemployment Insurance

_Updated Nov 11, 2025_
