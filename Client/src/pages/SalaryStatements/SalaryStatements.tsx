import { Box, Card, CardContent, CardHeader, Divider } from "@mui/material";
import SalaryForm from "./SalaryForm";
import { useTranslation } from "react-i18next";
import Result from "./Result";

const SalaryStatements = () => {
  const { t } = useTranslation();

  return (
    <div>
      <p>Muodosta palkkalaskelma yhdelle</p>
      <div>
        <Card className="salary-card">
          <CardHeader title={t("salary.form_title")} />
          <Divider />

          <CardContent>
            <Box>
              <SalaryForm />
              <Result />
            </Box>
          </CardContent>
        </Card>
      </div>
      <p>Muodosta palkkalaskelmat kaikille</p>
      <p>Hae palkkalaskelmat</p>
    </div>
  );
};

export default SalaryStatements;
