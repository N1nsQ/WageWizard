import { useSelector } from "react-redux";
import { selectUserName } from "../redux/slices/authSlice";
import { useTranslation } from "react-i18next";
import { Typography } from "@mui/material";

const Greetings = () => {
  const { t } = useTranslation();
  const user = useSelector(selectUserName);

  return (
    <Typography>
      {t("common.greetings")}
      {user}!
    </Typography>
  );
};

export default Greetings;
