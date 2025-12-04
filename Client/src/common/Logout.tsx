import { Box, Button } from "@mui/material";
import { useTranslation } from "react-i18next";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { logout } from "../redux/slices/authSlice";

const Logout = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const handleLogout = () => {
    dispatch(logout());
    navigate("/");
  };

  return (
    <div className="logout-container">
      <Box>
        <Button color="inherit" onClick={handleLogout}>
          {t("common.logout")}
        </Button>
      </Box>
    </div>
  );
};

export default Logout;
