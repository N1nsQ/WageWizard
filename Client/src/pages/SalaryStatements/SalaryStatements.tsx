import { Box, Card, CardContent, CardHeader, Divider } from "@mui/material";
import SalaryForm from "./SalaryForm";
import { useTranslation } from "react-i18next";

const SalaryStatements = () => {
  const { t } = useTranslation();

  return (
    <div>
      <div>
        <Card className="salary-card">
          <CardHeader title={t("salary.form_title")} />
          <Divider />

          <CardContent>
            <Box>
              <SalaryForm />
            </Box>
          </CardContent>
        </Card>
      </div>
    </div>
  );
};

export default SalaryStatements;
