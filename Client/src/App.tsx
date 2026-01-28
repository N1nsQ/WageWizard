// styles
import "./App.css";

// libraries
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

// pages
import { Home } from "./pages/Home/Home";
import Login from "./pages/Login/Login";
import { Typography } from "@mui/material";
import Employees from "./pages/Employees/Employees";
import EmployeeDetailsPage from "./pages/Employees/EmployeeDetails";
import SalaryStatements from "./pages/SalaryStatements/SalaryStatements";
import Layout from "./common/Layout";
import Timesheet from "./pages/Timesheet/Timesheet";

import { AuthProvider } from "./common/AuthProvider";
import Profile from "./pages/Employees/Profile";

function App() {
  return (
    <AuthProvider>
      <Router>
        <Layout>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/home" element={<Home />} />
            <Route path="/employees" element={<Employees />} />
            <Route path="/employees/:id" element={<EmployeeDetailsPage />} />
            <Route path="/profile/:id" element={<Profile />} />
            <Route path="/salarystatements" element={<SalaryStatements />} />
            <Route path="/timesheet" element={<Timesheet />} />
            <Route
              path="*"
              element={<Typography variant="h3">PAGE NOT FOUND</Typography>}
            />
          </Routes>
        </Layout>
      </Router>
    </AuthProvider>
  );
}

export default App;
