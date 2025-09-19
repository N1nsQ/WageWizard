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
        <div data-testid="username-field">
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
        </div>
        <div data-testid="password-field">
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
        </div>
        {authState.error && (
          <Typography
            data-testid="login-error-message"
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
