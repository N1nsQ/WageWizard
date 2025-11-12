import { AppBar, Box, Button, Container, Toolbar } from "@mui/material";
import { Link } from "react-router-dom";
import "../../App.css";

const NavBar = () => {
  return (
    <AppBar position="static" className="navbar">
      <Container>
        <Toolbar
          disableGutters
          sx={{ display: "flex", justifyContent: "space-between" }}
        >
          <Box>
            <Button color="inherit" component={Link} to="/home">
              Etusivu
            </Button>
            <Button color="inherit">Dokumentit</Button>
            <Button color="inherit" component={Link} to="/salarystatements">
              Palkkalaskelmat
            </Button>
            <Button color="inherit" component={Link} to="/employees">
              Työntekijät
            </Button>
          </Box>
          <Box>
            <Button color="inherit" component={Link} to="/">
              Log Out
            </Button>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default NavBar;
