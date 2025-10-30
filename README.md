## 🛠️ Tech Stack  
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)! [.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB) ![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white) ![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white) 

# WageWizard - Salary Calculator

🚧 This project is under active development 🚧  
  
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

### Databasse

- **SQL Server** – Stores employee information, payroll rates, and related payroll data
- **All data comes from the database** – Including employee info, payroll rates, and computed payroll details
- **Accessed via EF Core and LINQ queries** – Simplifies querying and filtering data
- **All scripts availbale**


## About

:hammer: C# | .NET Core | React | TypeScript | SQL Server | :hammer:  

- This project uses [microservice architecture](https://microservices.io/)
- This project uses BFF pattern (Backend for frontend) --> [An Introdiction to BFF Pattern](https://blog.bitsrc.io/bff-pattern-backend-for-frontend-an-introduction-e4fa965128bf) 

#### Backend
- Entity Framework
- Dependency Injection
- LINQ

### Frontend
- React, Vite
- Redux Toolkit

## Features
**Payroll Administrator**  
- create & save employment contracts
- Calculate monthly salary

**Employee**  
- download pdf / excel


## Tavoitteet

Suomessa palkansaajan bruttopalkan määrään vaikuttavat veroprosentti, asuinpaikkakunta, eläke- ja työttömyysvakuutusmaksut, peruskulut ja muut mahdolliset sivukulut. Tämä palkanlaskentasovellus pyrkii laskemaan maksettavan palkan määrän mahdollisimman tarkasti riippuen käyttäjän syöttämistä tiedoista.  

Käyttäjä syöttää nettopalkkansa ja perustietonsa (ikä, asuinkunta, tulotyyppi, verokortin mukainen veroprosentti), ja sovellus laskee:

* Arvioidun bruttopalkan
* Arvion maksettavista veroista ja sivukuluista
* Mahdollisuuden vertailla vaihtoehtoja (esim. eri kunnissa)

## Käytettävät teknologiat

* Käytetään Verohallinnon rajapintaa: Vero API
* Backend: ASP.NET Core
* Frontend: React, TypeScript

