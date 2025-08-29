import React, { useState } from "react";
import {
  Box,
  Card,
  CardContent,
  TextField,
  Button,
  Typography,
  Alert,
} from "@mui/material";

interface LoginRequest {
  username: string;
  password: string;
}

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault();

    if (!username || !password) {
      setError("Täytä käyttäjätunnus ja salasana");
      return;
    }

    console.log("Kirjautuminen:", { username, password });
    setError(null);
  };

  return (
    <>
      <div>
        <Typography variant="h1">Wage Wizard</Typography>
      </div>
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        minHeight="50vh"
        bgcolor="grey.100"
      >
        <Card sx={{ width: 400, borderRadius: 3, boxShadow: 4 }}>
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              Kirjaudu sisään
            </Typography>

            {error && (
              <Alert severity="error" sx={{ mb: 2 }}>
                {error}
              </Alert>
            )}

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
              onSubmit={handleLogin}
            >
              <TextField
                label="Käyttäjätunnus"
                variant="outlined"
                fullWidth
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
              <TextField
                label="Salasana"
                type="password"
                variant="outlined"
                fullWidth
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
              >
                Kirjaudu sisään
              </Button>
            </Box>
          </CardContent>
        </Card>
      </Box>
    </>
  );
}
