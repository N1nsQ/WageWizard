import { Grid, MenuItem, TextField } from "@mui/material";
import { Field, useForm, useFormState } from "react-final-form";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/store";
import { useTranslation } from "react-i18next";
import { useEffect } from "react";
import Result from "./Result";
import { fetchSalaryStatementCalculations } from "../../redux/slices/SalaryStatementSlice";

const SalaryStatementFields = () => {
  const form = useForm();
  const { values } = useFormState();
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();

  const employees = useSelector(
    (state: RootState) => state.salaryStatement.lookupEmployees
  );

  const salaryDetails = useSelector(
    (state: RootState) => state.salaryStatement.selectedEmployeeSalaryDetails
  );

  console.log("Salary Details: ", salaryDetails);

  useEffect(() => {
    if (values?.employeeId) {
      dispatch(fetchSalaryStatementCalculations(values.employeeId));
    }
  }, [values?.employeeId, dispatch]);

  useEffect(() => {
    if (values?.employeeId) {
      form.change("taxRate", salaryDetails?.taxPercent);
      form.change("grossSalary", salaryDetails?.grossSalary.toFixed(2));
      form.change("age", salaryDetails?.age);
      form.change("tyELPercent", salaryDetails?.tyELPercent);
      form.change(
        "unemploymentInsurancePercent",
        salaryDetails?.unemploymentInsurancePercent
      );
    } else {
      form.change("taxRate", "");
      form.change("grossSalary", "");
      form.change("age", "");
      form.change("tyELPercent", "");
      form.change("unemploymentInsurancePercent", "");
    }
  }, [salaryDetails, values?.employeeId, form]);

  return (
    <div>
      <Grid container spacing={2}>
        <Grid size={{ xs: 12 }}>
          <Field name="employeeId">
            {({ input }) => (
              <TextField
                {...input}
                select
                fullWidth
                variant="outlined"
                label={t("salary.select_employee")}
              >
                <MenuItem value="">
                  <em>{t("salary.no_selection")}</em>
                </MenuItem>
                {employees?.map((emp) => (
                  <MenuItem key={emp.id} value={emp.id}>
                    {emp.fullName}
                  </MenuItem>
                ))}
              </TextField>
            )}
          </Field>
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <Field name="taxRate">
            {({ input }) => (
              <TextField
                {...input}
                fullWidth
                type="number"
                label={t("salary.tax_percentage")}
                variant="outlined"
              />
            )}
          </Field>
        </Grid>

        <Grid size={{ xs: 12, sm: 4 }}>
          <Field name="grossSalary">
            {({ input }) => (
              <TextField
                {...input}
                fullWidth
                type="number"
                label={t("salary.gross_salary")}
                variant="outlined"
              />
            )}
          </Field>
        </Grid>

        <Grid size={{ xs: 12, sm: 4 }}>
          <Field name="age">
            {({ input }) => (
              <TextField
                {...input}
                fullWidth
                type="number"
                label={t("salary.age")}
                variant="outlined"
              />
            )}
          </Field>
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <Field name="tyELPercent">
            {({ input }) => (
              <TextField
                {...input}
                fullWidth
                type="number"
                label={t("salary.employee_pension_contribution")}
                variant="outlined"
              />
            )}
          </Field>
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <Field name="unemploymentInsurancePercent">
            {({ input }) => (
              <TextField
                {...input}
                fullWidth
                type="number"
                label={t("salary.unemployment_contribution")}
                variant="outlined"
              />
            )}
          </Field>
        </Grid>
      </Grid>
      <div>
        <Result employeeId={values?.employeeId} />
      </div>
    </div>
  );
};

export default SalaryStatementFields;
