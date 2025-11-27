import { Grid, MenuItem, TextField } from "@mui/material";
import { Field, useForm, useFormState } from "react-final-form";
import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store";
import { useTranslation } from "react-i18next";
import { useEffect } from "react";
import Result from "./Result";

const SalaryStatementFields = () => {
  const form = useForm();
  const { values } = useFormState();
  const { t } = useTranslation();

  const { data: employees } = useSelector(
    (state: RootState) => state.employeesSalaryDetails
  );

  useEffect(() => {
    const selectedEmployee = employees?.find(
      (emp) => emp.id === values?.employeeId
    );

    if (selectedEmployee) {
      const tyelPercent = Number(
        (selectedEmployee.tyELPercent * 100).toFixed(2)
      );
      const unemploymentPercent = Number(
        (selectedEmployee.unemploymentInsurancePercent * 100).toFixed(2)
      );

      form.change("veroprosentti", selectedEmployee.taxPercentage);
      form.change("peruspalkka", selectedEmployee.salaryAmount.toFixed(2));
      form.change("age", selectedEmployee.age);
      form.change("tyELPercent", tyelPercent.toFixed(2));
      form.change("unemploymentInsurance", unemploymentPercent.toFixed(2));
    } else {
      form.change("veroprosentti", "");
      form.change("peruspalkka", "");
    }
  }, [values?.employeeId, employees, form]);

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
                    {emp.firstName} {emp.lastName}
                  </MenuItem>
                ))}
              </TextField>
            )}
          </Field>
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <Field name="veroprosentti">
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
          <Field name="peruspalkka">
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
          <Field name="unemploymentInsurance">
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
