import { Box, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import wizard from "../../public/wizard.png";

const WageWizard = () => {
  const { t } = useTranslation();
  return (
    <Box
      display="flex"
      alignItems="center"
      justifyContent="center"
      sx={{ marginBottom: 5, gap: 2 }}
    >
      <Typography variant="h1">{t("common.wage_wizard")}</Typography>
      <Box
        component="img"
        src={wizard}
        alt="Wizard"
        sx={{
          width: "50%",
          maxWidth: 120,
          height: "auto",
        }}
      />
    </Box>
  );
};

export default WageWizard;
