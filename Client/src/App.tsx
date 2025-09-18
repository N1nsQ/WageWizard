// styles
import "./App.css";

// libraries
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

// pages
import { Home } from "./pages/Home/Home";
import Login from "./pages/Login/Login";
import { Typography } from "@mui/material";
import Employees from "./pages/Employees/Employees";

function App() {
  return (
    <>
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/home" element={<Home />} />
          <Route path="/employees" element={<Employees />} />
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
