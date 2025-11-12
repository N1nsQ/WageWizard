import { useLocation } from "react-router-dom";
import NavBar from "./NavBar/NavBar";
import type { ReactNode } from "react";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  const location = useLocation();

  const hideNavBar = location.pathname === "/";

  return (
    <>
      {!hideNavBar && <NavBar />}
      {children}
    </>
  );
};

export default Layout;
