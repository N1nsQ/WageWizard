import { useSelector } from "react-redux";
import type { RootState } from "../redux/store";
import type { AuthContextType } from "../models/AuthContextType";
import { AuthContext } from "./AuthContext";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const { role, isAuthenticated } = useSelector(
    (state: RootState) => state.auth
  );

  const value: AuthContextType = {
    role,
    isAuthenticated,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};
