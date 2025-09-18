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
import { useTranslation } from "react-i18next";

const Login: React.FC = () => {
  const { t } = useTranslation();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { isLoading } = useSelector((state: RootState) => state.auth);

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
      <Box className="login-container">
        <Typography variant="h4" mb={2}>
          {t("login.title")}
        </Typography>
        <TextField
          label={t("login.username")}
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          margin="normal"
        />
        <TextField
          label={t("login.password")}
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          margin="normal"
        />
        {authState.error && (
          <Typography className="error-message" variant="body2">
            {t("backend_error_messages.invalid_username")}
          </Typography>
        )}
        <Button variant="contained" onClick={handleLogin} sx={{ mt: 2 }}>
          {t("login.login")}
        </Button>

        {isLoading && (
          <div className="loading-overlay">
            {authState.isLoading && <CircularProgress color="inherit" />}
          </div>
        )}
      </Box>
    </div>
  );
};

export default Login;
