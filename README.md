![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB) ![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white) ![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white) 
# WageWizard - Salary Calculator

🚧 This project is under active development 🚧  
💻 See [Setup & Configuration](https://github.com/N1nsQ/WageWizard/blob/main/Documentation/setup_instructions.md) for details on running WageWizard locally.  
💡 See [development history](https://github.com/N1nsQ/WageWizard/blob/main/Documentation/development_history.md) to peek the most newest updates and how all have been changed. This page is updated when all features of new release are ready and working, but for those who are curious there might be some teasers 🍬

#### SonarQube Statistics for backend: 
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=N1nsQ_WageWizard&metric=bugs)](https://sonarcloud.io/summary/new_code?id=N1nsQ_WageWizard)  
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=N1nsQ_WageWizard&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=N1nsQ_WageWizard)  
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=N1nsQ_WageWizard&metric=coverage)](https://sonarcloud.io/summary/new_code?id=N1nsQ_WageWizard)  
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=N1nsQ_WageWizard&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=N1nsQ_WageWizard)  

#### SonarQube Statistics for frontend: 
(coming soon)
  
## Wage Wizard 1.0 now available!
<img width="1182" height="886" alt="image" src="https://github.com/user-attachments/assets/27191215-f21a-4864-9cc8-a16400462827" />


Layout updated 12 Nov 2025

  

#### Features:

- Login with test user
- List of employees
- Employee details page
- Calculate salary for selected employee and view payroll calculation results
- Finnish / English translations

#### Frontend
- **React** – Component-based UI
- **Vite** – Fast development server and build tool
- **TypeScript** – Type safety and intelligent code completion
- **Redux Toolkit** – State management
- **Final Form** – Form handling and form state management
- **MUI (Material UI)** – UI components and layout
- **i18next** – Internationalization
- **React Router Dom** – Routing between pages
- **Vitest** – Unit and integration testing

#### Backend

- **C# / ASP.NET Core** – Web API and application logic (.NET 8.0)
- **Entity Framework Core** - ORM for database access
- **LINQ** – Querying database entities
- **xUnit** – Unit testing
- **BFF pattern (Backend for frontend)** – [An Introdiction to BFF Pattern](https://blog.bitsrc.io/bff-pattern-backend-for-frontend-an-introduction-e4fa965128bf)
- **Layered (N-Tier) Architecture** - [Dev.to](https://dev.to/dotnetfullstackdev/layered-n-tier-architecture-in-net-core-51ic)**

#### Databasse

- **SQL Server** – Stores employee information, payroll rates, and related payroll data
- **All data comes from the database** – Including employee info, payroll rates, and computed payroll details
- **Accessed via EF Core and LINQ queries** – Simplifies querying and filtering data
- **All scripts available**

#### Quality
- **SonarQube** – Continuous code quality analysis for identifying bugs, vulnerabilities, code smells, and monitoring test coverage
- **Coverlet** –  Code coverage tool for backend (.NET)
- **Secret Manager** – Hide sensitive information

## About WageWizard

### Login
<img width="1068" height="902" alt="image" src="https://github.com/user-attachments/assets/40280f4c-ee94-45ae-aa76-cf134f8e0228" />  
Users log in with the provided credentials. Creating a personal account is currently not available. The application can be tested using the test credentials. (See Setup & Configuration page in documentation.)

### Employees Page
<img width="1466" height="713" alt="image" src="https://github.com/user-attachments/assets/2e628437-5a8d-4ca7-a47b-bb6bf1b305ea" />  
Displays a list of all employees showing minimal information: first name, last name, job title, and email. If an employee has an image, it is shown; otherwise, a default image is used. Each employee entry includes a link to their detailed profile page.

<img width="1984" height="1650" alt="image" src="https://github.com/user-attachments/assets/fe9d0b7c-adbc-4fd4-a837-175549705a49" />
Add new Employee

### Employee Details
<img width="737" height="1545" alt="image" src="https://github.com/user-attachments/assets/b27b72c1-2c51-435b-b251-f2af5116744d" />  
  
This view shows detailed information about the selected employee. Editing the information is not currently available.

### Payroll Page
<img width="1528" height="1100" alt="image" src="https://github.com/user-attachments/assets/e7dbf223-8ba4-4789-a511-397dc32af368" />  
<img width="1521" height="1289" alt="image" src="https://github.com/user-attachments/assets/63e67001-9e9b-44ea-a0a3-9695c8219ccf" />
  
The user selects an employee from the list whose salary they want to calculate. The rest of the form updates automatically based on the selected employee, and the application calculates the salary, showing all applicable deductions (see the first image in this document). All data comes from the database and cannot be edited.


## 🚀 Coming Features

Planned future updates will expand Wage Wizard into a more complete and versatile payroll management system. The upcoming features will enhance employee management, automate complex payroll calculations, and provide powerful reporting and data export tools for deeper financial insights.

#### 👥 Employee Management
- Add new employees
- Edit employee information
- Mark employees as inactive / no longer employed
- Track job titles and department history

#### 💰 Payroll Enhancements
- Generate salary statements for all employees at once
- Download salary statements as PDF files
- Add support for hourly and monthly salary types
- Automatic calculation of holiday pay and sick leave
- Include fringe benefits, bonuses, and overtime pay
- Handle unpaid leave and deductions
- Save and view salary history per employee

#### 📊 Reporting & Data Export
- Export payroll data to Excel or CSV
- Payroll summary report by month or department
- Visual charts of total salary expenses
- Tax and insurance cost analysis










