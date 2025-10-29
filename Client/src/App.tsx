// styles
import "./App.css";

// libraries
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

// pages
import { Home } from "./pages/Home/Home";
import Login from "./pages/Login/Login";
import { Typography } from "@mui/material";
import Employees from "./pages/Employees/Employees";
import NavBar from "./common/NavBar/NavBar";
import EmployeeDetailsPage from "./pages/Employees/EmployeeDetails";
import SalaryStatements from "./pages/SalaryStatements/SalaryStatements";

function App() {
  return (
    <>
      <NavBar />
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/home" element={<Home />} />
          <Route path="/employees" element={<Employees />} />
          <Route path="/employees/:id" element={<EmployeeDetailsPage />} />
          <Route path="/salarystatements" element={<SalaryStatements />} />
          <Route
            path="*"
            element={<Typography variant="h3">PAGE NOT FOUND</Typography>}
          />
        </Routes>
      </Router>
    </>
  );
}

export default App;
