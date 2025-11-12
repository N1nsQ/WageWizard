import { AppBar, Box, Button, Container, Toolbar } from "@mui/material";
import { Link } from "react-router-dom";
import "../../App.css";
import { useTranslation } from "react-i18next";

const NavBar = () => {
  const { t } = useTranslation();

  return (
    <AppBar position="static" className="navbar">
      <Container>
        <Toolbar
          disableGutters
          sx={{ display: "flex", justifyContent: "center" }}
        >
          <Box>
            <Button color="inherit" component={Link} to="/home">
              {t("common.front_page")}
            </Button>
            <Button color="inherit">{t("common.documents")}</Button>
            <Button color="inherit" component={Link} to="/salarystatements">
              {t("common.salary_statements")}
            </Button>
            <Button color="inherit" component={Link} to="/employees">
              {t("common.employees")}
            </Button>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default NavBar;
