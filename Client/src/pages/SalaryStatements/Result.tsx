import { CircularProgress, Grid, Paper, Typography } from "@mui/material";
import { fetchSalaryStatementCalculations } from "../../redux/slices/SalaryStatementSlice";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/store";

interface ResultProps {
  employeeId: string | null;
}

const Result = ({ employeeId }: ResultProps) => {
  const dispatch = useDispatch<AppDispatch>();

  const { data, isLoading, error } = useSelector(
    (state: RootState) => state.salaryStatementCalculation
  );

  // 🔹 Hae palkkalaskelma kun työntekijä vaihtuu
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
        <Typography>Valitse työntekijä nähdäksesi palkkalaskelman.</Typography>
      </Paper>
    );
  }

  console.log("Data: ", data);

  return (
    <Paper sx={{ padding: 2, marginTop: 2 }}>
      <Typography variant="h6" gutterBottom>
        Palkkalaskelma
      </Typography>
      <Grid container spacing={1}>
        <Grid size={{ xs: 6 }}>
          <Typography>Ennakonpidätys</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">
            {data.withholdingTax.toFixed(2)} €
          </Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography>Työeläkemaksu</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">{data.tyELAmount.toFixed(2)} €</Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography>Työttömyysvakuutusmaksu</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right">
            {data.unemploymentInsuranceAmount.toFixed(2)} €
          </Typography>
        </Grid>

        <Grid size={{ xs: 6 }}>
          <Typography fontWeight="bold">Nettopalkka</Typography>
        </Grid>
        <Grid size={{ xs: 6 }}>
          <Typography align="right" fontWeight="bold">
            {data.netSalary.toFixed(2)} €
          </Typography>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default Result;
