import { Box, Card, CardContent, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

export function Home() {
  const { t } = useTranslation();

  return (
    <>
      <div>
        <Typography variant="h4">{t("home.title")}</Typography>
      </div>
      <div>
        <Card sx={{ width: 400, borderRadius: 3, boxShadow: 4 }}>
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              {t("home.contracts")}
            </Typography>

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
            ></Box>
          </CardContent>
        </Card>
      </div>
      <div>
        <Card
          component={Link}
          to="/salarystatements"
          sx={{
            display: "block",
            width: 400,
            borderRadius: 3,
            boxShadow: 4,
            textDecoration: "none",
            color: "inherit",
            transition: "transform 0.2s, box-shadow 0.2s",
            "&:hover": {
              transform: "scale(1.02)",
              boxShadow: 6,
            },
          }}
        >
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              {t("home.salary_statements")}
            </Typography>

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
            ></Box>
          </CardContent>
        </Card>
      </div>

      <div>
        <Card
          component={Link}
          to="/employees"
          sx={{
            display: "block",
            width: 400,
            borderRadius: 3,
            boxShadow: 4,
            textDecoration: "none",
            color: "inherit",
            transition: "transform 0.2s, box-shadow 0.2s",
            "&:hover": {
              transform: "scale(1.02)",
              boxShadow: 6,
            },
          }}
        >
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              {t("home.employees")}
            </Typography>

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
            ></Box>
          </CardContent>
        </Card>
      </div>
    </>
  );
}
