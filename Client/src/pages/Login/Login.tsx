import React, { useState, useEffect } from "react";
import { TextField, Button, Typography, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { loginUser } from "../../redux/slices/authSlice";
import type { RootState, AppDispatch } from "../../redux/store";
import type { LoginDto } from "../../models/LoginDto";

const Login: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

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
    </Box>
  );
};

export default Login;
