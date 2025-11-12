import { Box, Button } from "@mui/material";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

const Logout = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();

  const handleLogout = () => {
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
