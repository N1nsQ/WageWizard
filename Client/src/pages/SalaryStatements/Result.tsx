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

  const { data, isLoading, error } = useSelector(
    (state: RootState) => state.salaryStatementCalculation
  );

  useEffect(() => {
    if (employeeId) {
      dispatch(fetchSalaryStatementCalculations(employeeId));
    }
  }, [employeeId, dispatch]);

  if (isLoading) {
    return (
      <Paper sx={{ padding: 2, marginTop: 2, textAlign: "center" }}>
        <CircularProgress />
      </Paper>
    );
  }

  if (error) {
    return (
      <Paper sx={{ padding: 2, marginTop: 2 }}>
        <Typography color="error">Virhe: {error}</Typography>
      </Paper>
    );
  }

  if (!data) {
    return (
      <Paper sx={{ padding: 2, marginTop: 2 }}>
        <Typography>{t("salary.select_employee_tooltip")}</Typography>
      </Paper>
    );
  }

  console.log("Data: ", data);

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
          <Typography align="right">{data.withholdingTax} €</Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography>{t("salary.tyel_contribution")}</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">{data.tyELAmount} €</Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography>
            {t("salary.unemployment_insurance_contribution")}
          </Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">
            {data.unemploymentInsuranceAmount} €
          </Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography fontWeight="bold">{t("salary.net_salary")}</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right" fontWeight="bold">
            {data.netSalary} €
          </Typography>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default Result;
