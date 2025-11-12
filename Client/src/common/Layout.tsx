import { useLocation } from "react-router-dom";
import NavBar from "./NavBar/NavBar";
import type { ReactNode } from "react";
import LanguageSelector from "./LanguageSelector/LanguageSelector";
import { Box, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import Logout from "./Logout";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  const location = useLocation();
  const { t } = useTranslation();

  const hideNavBar = location.pathname === "/";

  return (
    <div>
      <Box className="top-bar">
        <LanguageSelector />
        {!hideNavBar && <Logout />}
      </Box>
      <Box>
        <Typography variant="h2" sx={{ marginBottom: 5 }}>
          {t("common.wage_wizard")}
        </Typography>
      </Box>

      {!hideNavBar && <NavBar />}
      {children}
    </div>
  );
};

export default Layout;
