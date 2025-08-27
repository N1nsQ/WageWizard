import "./App.css";
import Login from "./pages/Login/Login";
import { Typography } from "@mui/material";

function App() {
  return (
    <>
      <div>
        <Typography variant="h1">WageWizard</Typography>
      </div>
      <div>
        <Login />
      </div>
    </>
  );
}

export default App;
