import { Form } from "react-final-form";
import LoginForm from "./LoginForm";
import { Button } from "@mui/material";
import { useTranslation } from "react-i18next";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../../redux/store";
import { loginUser } from "../../../redux/slices/authSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import LoadingOverlay from "../../../common/LoadingOverlay";

const LoginFormWrapper = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();
  const authState = useSelector((state: RootState) => state.auth);

  const navigate = useNavigate();

  const onSubmit = async (values: { username: string; password: string }) => {
    dispatch(loginUser(values));
  };

  useEffect(() => {
    if (authState.isAuthenticated) {
      navigate("/home");
    }
  }, [authState.isAuthenticated, navigate]);

  return (
    <div>
      <Form
        onSubmit={onSubmit}
        render={({ handleSubmit }) => (
          <form onSubmit={handleSubmit}>
            <LoginForm authState={authState} />
            <Button type="submit" variant="contained" sx={{ mt: 2 }}>
              {t("login.login")}
            </Button>
            <LoadingOverlay isLoading={authState.isLoading} />
          </form>
        )}
      />
    </div>
  );
};

export default LoginFormWrapper;
