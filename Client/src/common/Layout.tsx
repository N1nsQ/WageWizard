import { useLocation } from "react-router-dom";
import NavBar from "./NavBar/NavBar";
import type { ReactNode } from "react";
import LanguageSelector from "./LanguageSelector/LanguageSelector";
import { Box } from "@mui/material";
import Logout from "./Logout";
import WageWizard from "./WageWizard";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  const location = useLocation();
  const hideNavBar = location.pathname === "/";

  return (
    <div>
      <Box className="top-bar">
        <LanguageSelector />
        {!hideNavBar && <Logout />}
      </Box>
      <Box>
        <WageWizard />
      </Box>

      {!hideNavBar && <NavBar />}
      {children}
    </div>
  );
};

export default Layout;
