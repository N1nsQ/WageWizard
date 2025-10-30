# WageWizard - Salary Calculator

🚧 This project is under active development 🚧  

## 🛠️ Tech Stack  
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB) ![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white) ![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white) 
  
## Wage Wizard 1.0 now available!

<img width="1231" height="1220" alt="image" src="https://github.com/user-attachments/assets/65985ab5-2960-45ae-8221-6cb4f5a93a16" />  

### Features:

- Login with test user
- List of employees
- Employee details page
- Calculate salary for selected employee and view payroll calculation results
- Finnish / English translations

### Frontend
- **React** – Component-based UI
- **Vite** – Fast development server and build tool
- **TypeScript** – Type safety and autocomplete
- **Redux Toolkit** – State management
- **Final Form** – Form handling and form state management
- **MUI (Material UI)** – UI components and layout
- **i18next** – Internationalization
- **React Router Dom** – Routing between pages
- **Vitest** – Unit and integration testing

### Backend

- **C# / ASP.NET Core** – Web API and application logic
- **Entity Framework Core** - ORM for database access
- **LINQ** – Querying database entities
- **xUnit** – Unit testing
- **BFF pattern (Backend for frontend)** – [An Introdiction to BFF Pattern](https://blog.bitsrc.io/bff-pattern-backend-for-frontend-an-introduction-e4fa965128bf) 

### Databasse

- **SQL Server** – Stores employee information, payroll rates, and related payroll data
- **All data comes from the database** – Including employee info, payroll rates, and computed payroll details
- **Accessed via EF Core and LINQ queries** – Simplifies querying and filtering data
- **All scripts availbale**

### About

#### Login
<img width="1068" height="902" alt="image" src="https://github.com/user-attachments/assets/40280f4c-ee94-45ae-aa76-cf134f8e0228" />  
Users log in with the provided credentials. Creating a personal account is currently not available. The application can be tested using the test credentials:  
  
**Username:** Tessa Testaaja  
**Password:** SalainenSalasana987!  

#### Employees Page
<img width="1466" height="713" alt="image" src="https://github.com/user-attachments/assets/2e628437-5a8d-4ca7-a47b-bb6bf1b305ea" />  
Displays a list of all employees showing minimal information: first name, last name, job title, and email. If an employee has an image, it is shown; otherwise, a default image is used. Each employee entry includes a link to their detailed profile page.

#### Employee Details
<img width="737" height="1545" alt="image" src="https://github.com/user-attachments/assets/b27b72c1-2c51-435b-b251-f2af5116744d" />  
  
This view shows detailed information about the selected employee. Editing the information is not currently available.

#### Payroll Page
<img width="1295" height="792" alt="image" src="https://github.com/user-attachments/assets/6e4ddf7a-2da0-4d4c-996b-c9df4f195732" />  
The user selects an employee from the list whose salary they want to calculate. The rest of the form updates automatically based on the selected employee, and the application calculates the salary, showing all applicable deductions (see the first image in this document). All data comes from the database and cannot be edited.


## 🚀 Coming Features

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

#### 🧭 UI & Usability
- Navigation bar for improved navigation and accessibility
- Logo and branding
- Light mode / dark mode










