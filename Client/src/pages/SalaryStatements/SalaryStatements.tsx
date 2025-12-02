import { Box, Card, CardContent, CardHeader, Divider } from "@mui/material";
import { useTranslation } from "react-i18next";
import { Form } from "react-final-form";
import SalaryStatementFields from "./SalaryStatementFields";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../redux/store";
import { useEffect } from "react";
import { fetchEmployeesLookup } from "../../redux/slices/SalaryStatementSlice";

const SalaryStatements = () => {
  const { t } = useTranslation();

  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    dispatch(fetchEmployeesLookup());
  }, [dispatch]);

  const onSubmit = () => {
    console.log("Lomaketta klikattu");
  };

  return (
    <div>
      <div>
        <Card className="salary-card">
          <CardHeader title={t("salary.form_title")} />
          <Divider />

          <CardContent>
            <Box>
              <Form
                onSubmit={onSubmit}
                render={({ handleSubmit }) => (
                  <form onSubmit={handleSubmit}>
                    <SalaryStatementFields />
                  </form>
                )}
              />
            </Box>
          </CardContent>
        </Card>
      </div>
    </div>
  );
};

export default SalaryStatements;
