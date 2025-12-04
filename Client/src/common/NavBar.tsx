import { AppBar, Box, Button, Container, Toolbar } from "@mui/material";
import { Link } from "react-router-dom";
import "../App.css";
import { useTranslation } from "react-i18next";
import { useAuth } from "../hooks/authHook";

const NavBar = () => {
  const { t } = useTranslation();
  const { role } = useAuth();

  return (
    <AppBar position="static" className="navbar">
      <Container>
        <Toolbar
          disableGutters
          sx={{ display: "flex", justifyContent: "center" }}
        >
          {/* Common pages */}
          <Box>
            <Button color="inherit" component={Link} to="/home">
              {t("common.front_page")}
            </Button>

            {/* Employees */}
            {role === "Employee" && (
              <Button color="inherit" component={Link} to="/timesheet">
                {t("common.timesheet")}
              </Button>
            )}
            {role === "Employee" && (
              <Button color="inherit" component={Link} to="/">
                {t("common.profile")}
              </Button>
            )}
            {role === "Employee" && (
              <Button color="inherit" component={Link} to="/">
                {t("common.salary")}
              </Button>
            )}

            {/* Payroll */}
            {(role === "TestUser" ||
              role === "Admin" ||
              role === "Payroll") && (
              <Button color="inherit">{t("common.documents")}</Button>
            )}
            {(role === "TestUser" ||
              role === "Admin" ||
              role === "Payroll") && (
              <Button color="inherit" component={Link} to="/salarystatements">
                {t("common.salary_statements")}
              </Button>
            )}
            {(role === "TestUser" ||
              role === "Admin" ||
              role === "Payroll") && (
              <Button color="inherit" component={Link} to="/employees">
                {t("common.employees")}
              </Button>
            )}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default NavBar;
