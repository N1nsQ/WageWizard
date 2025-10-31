# Setup & Configuration

Follow these steps to set up and run the Wage Wizard application locally.

## 🧩 Prerequisites

These are the tools I used to build Wage Wizard, and I recommend using the same setup when testing the application.
You can use similar alternatives, but the following instructions assume you are using the same tech stack.

- Node.js (v18 or later)
- npm or yarn
- .NET SDK 8.0+
- SQL Server
- Visual Studio for backend development
- Visual Studio Code for frontend development

After that you can clone the repo:

```bash
git clone https://github.com/N1nsQ/WageWizard.git
```

## Setup database

- On **Windows**, you can run the batch script to build the database:
  - Run `build_database.bat` located in the root folder.
- On other systems, run the SQL scripts found in the DB folder manually.

## Setup backend

- Edit the `appsettings.json` file and update the connection string with your database name.
- Open `Program.cs` and review the `AddCors` configuration.
  - Replace `.WithOrigins("http://localhost:5173")` with the correct origin and port used by your frontend application.

## Setup frontend

- install all required packages: `npm install`
- create a `.env` file in the project root and define API_BASE url.
  - `VITE_API_BASE=https://localhost:0000`
  - ⚠️ Make sure the API URL matches your backend server address and port.

## Run the Application

- Start backend service
- Start frontend with `npm run dev`
- It is currently not possible to create a new user. You can log in using the following test credentials:
  - **Username:** Tessa Testaaja
  - **Password:** SalainenSalasana987!
- Alternatively, you can manually add a new user to the Users table in the database.
