import { Box, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import { LanguageSelector } from "../LanguageSelector/LanguageSelector";

const NavBar = () => {
  const { t } = useTranslation();

  return (
    <Box
      display="flex"
      alignItems="center"
      justifyContent="center"
      position="relative"
      p={2}
    >
      <Typography variant="h1" component="h1">
        {t("common.wage_wizard")}
      </Typography>
      <Box position="absolute" right={16}>
        <LanguageSelector />
      </Box>
    </Box>
  );
};

export default NavBar;
