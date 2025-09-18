import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  Typography,
  Box,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { loginUser } from "../../redux/slices/authSlice";
import type { RootState, AppDispatch } from "../../redux/store";
import type { LoginDto } from "../../models/LoginDto";

const Login: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { isLoading, error } = useSelector((state: RootState) => state.auth);

  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();
  const authState = useSelector((state: RootState) => state.auth);

  const handleLogin = () => {
    const loginData: LoginDto = { username, password };
    dispatch(loginUser(loginData));
  };

  useEffect(() => {
    if (authState.isAuthenticated) {
      navigate("/home");
    }
  }, [authState.isAuthenticated, navigate]);

  return (
    <div>
      <Box
        display="flex"
        flexDirection="column"
        maxWidth={400}
        margin="auto"
        mt={10}
      >
        <Typography variant="h4" mb={2}>
          Login
        </Typography>
        <TextField
          label="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          margin="normal"
        />
        <TextField
          label="Password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          margin="normal"
        />
        {authState.error && (
          <Typography color="error" variant="body2">
            {authState.error}
          </Typography>
        )}
        <Button variant="contained" onClick={handleLogin} sx={{ mt: 2 }}>
          Login
        </Button>

        {isLoading && (
          <div
            style={{
              position: "fixed",
              top: 0,
              left: 0,
              width: "100vh",
              height: "100vh",
              backgroundColor: "rgba(0, 0, 0, 0.4)",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              zIndex: 9999,
              color: "#fff",
            }}
          >
            <CircularProgress color="inherit" />
          </div>
        )}
        {error && <p style={{ color: "red" }}>{error}</p>}
      </Box>
    </div>
  );
};

export default Login;
