import { Box, Typography } from "@mui/material";

import { useTranslation } from "react-i18next";
import LoginFormWrapper from "./components/LoginFormWrapper";

const Login = () => {
  const { t } = useTranslation();

  return (
    <div>
      <Box className="login-container">
        <Typography variant="h4" mb={2}>
          {t("login.title")}
        </Typography>
        <LoginFormWrapper />
      </Box>
    </div>
  );
};

export default Login;
