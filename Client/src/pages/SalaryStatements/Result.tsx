import { CircularProgress, Grid, Paper, Typography } from "@mui/material";
import { fetchSalaryStatementCalculations } from "../../redux/slices/SalaryStatementSlice";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/store";
import { useTranslation } from "react-i18next";

interface ResultProps {
  employeeId: string | null;
}

const Result = ({ employeeId }: ResultProps) => {
  const dispatch = useDispatch<AppDispatch>();
  const { t } = useTranslation();

  const salaryDetails = useSelector(
    (state: RootState) => state.salaryStatement.selectedEmployeeSalaryDetails
  );

  useEffect(() => {
    if (employeeId) {
      dispatch(fetchSalaryStatementCalculations(employeeId));
    }
  }, [employeeId, dispatch]);

  // if (isLoading) {
  //   return (
  //     <Paper sx={{ padding: 2, marginTop: 2, textAlign: "center" }}>
  //       <CircularProgress />
  //     </Paper>
  //   );
  // }

  // if (error) {
  //   return (
  //     <Paper sx={{ padding: 2, marginTop: 2 }}>
  //       <Typography color="error">Virhe: {error}</Typography>
  //     </Paper>
  //   );
  // }

  // if (!data) {
  //   return (
  //     <Paper sx={{ padding: 2, marginTop: 2 }}>
  //       <Typography>{t("salary.select_employee_tooltip")}</Typography>
  //     </Paper>
  //   );
  // }

  return (
    <Paper sx={{ padding: 2, marginTop: 2 }}>
      <Typography variant="h6" gutterBottom>
        {t("salary.salary_statement")}
      </Typography>
      <Grid container spacing={1}>
        <Grid size={{ xs: 6 }}>
          <Typography>{t("salary.withholding_tax")}</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">
            {salaryDetails?.withholdingTax.toFixed(2)} €
          </Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography>{t("salary.tyel_contribution")}</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">
            {salaryDetails?.tyELAmount.toFixed(2)} €
          </Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography>
            {t("salary.unemployment_insurance_contribution")}
          </Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">
            {salaryDetails?.unemploymentInsuranceAmount.toFixed(2)} €
          </Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography fontWeight="bold">{t("salary.net_salary")}</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right" fontWeight="bold">
            {salaryDetails?.netSalary.toFixed(2)} €
          </Typography>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default Result;
