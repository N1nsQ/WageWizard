import { TextField, Typography } from "@mui/material";
import { Field } from "react-final-form";
import { useTranslation } from "react-i18next";
import type { AuthState } from "../../../models/AuthState";

interface LoginFormProps {
  authState: AuthState;
}

const LoginForm = ({ authState }: LoginFormProps) => {
  const { t } = useTranslation();

  return (
    <div>
      <div>
        <Field name="username">
          {({ input, meta }) => (
            <TextField
              {...input}
              label={t("login.username")}
              margin="normal"
              error={meta.touched && meta.error ? true : false}
              helperText={meta.touched && meta.error ? meta.error : ""}
              fullWidth
            />
          )}
        </Field>
        <Field name="password">
          {({ input, meta }) => (
            <TextField
              {...input}
              type="password"
              label={t("login.password")}
              margin="normal"
              error={meta.touched && meta.error ? true : false}
              helperText={meta.touched && meta.error ? meta.error : ""}
              fullWidth
            />
          )}
        </Field>
        {authState.error && (
          <Typography
            className="error-message"
            variant="body2"
            color="error"
            sx={{ mt: 1 }}
          >
            {t("backend_error_messages.invalid_username")}
          </Typography>
        )}
      </div>
    </div>
  );
};

export default LoginForm;
