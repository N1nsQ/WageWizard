CREATE TABLE Payrolls
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NULL,
    SalaryDate DATETIME2 NULL,
    PayPeriod DATETIME2 NULL,
    BaseSalary DECIMAL(18,2) NULL,
    MealBenefit DECIMAL(18,2) NULL,
    PhoneBenefit DECIMAL(18,2) NULL,
    OvertimePay DECIMAL(18,2) NULL,
    GrossSalary DECIMAL(18,2) NULL,
    NetSalary DECIMAL(18,2) NULL,
    WithholdingTax DECIMAL(18,2) NULL,
    EmployeePensionContribution DECIMAL(18,2) NULL,
    UnemploymentInsuranceContribution DECIMAL(18,2) NULL,
    UnionMembershipFee DECIMAL(18,2) NULL
);