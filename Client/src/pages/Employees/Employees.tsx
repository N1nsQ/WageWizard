import { Typography } from "@mui/material";
import { useTranslation } from "react-i18next";

const Employees = () => {
  const { t } = useTranslation();

  return (
    <div>
      <Typography variant="h4">{t("employees.title")}</Typography>
    </div>
  );
};

export default Employees;
