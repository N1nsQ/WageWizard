import { Button, Grid, MenuItem, TextField } from "@mui/material";
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

  const handleChange = () => {
    console.log("HandleChange");
  };

  useEffect(() => {
    const selectedEmployee = employees?.find(
      (emp) => emp.id === values?.employeeId
    );

    console.log("Selected Employee:", selectedEmployee);

    if (selectedEmployee) {
      const tyelPercent = Number(
        (selectedEmployee.tyELPercent * 100).toFixed(2)
      );
      const unemploymentPercent = Number(
        (selectedEmployee.unemploymentInsurance * 100).toFixed(2)
      );

      console.log("Employee TVM: ", unemploymentPercent);

      form.change("veroprosentti", selectedEmployee.taxPercentage);
      form.change("peruspalkka", selectedEmployee.salaryAmount);
      form.change("age", selectedEmployee.age);
      form.change("tyELPercent", tyelPercent);
      form.change("unemploymentInsurance", unemploymentPercent);
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
                  <em>Ei valintaa</em>
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
                label="Veroprosentti"
                variant="outlined"
                onChange={handleChange}
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
                label="Peruspalkka"
                variant="outlined"
                onChange={handleChange}
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
                label="Ikä"
                variant="outlined"
                onChange={handleChange}
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
                label="TyEL %"
                variant="outlined"
                onChange={handleChange}
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
                label="Työttömyysvakuutus %"
                variant="outlined"
                onChange={handleChange}
              />
            )}
          </Field>
        </Grid>
      </Grid>
      <div>
        <Button>{t("salary.calculate")}</Button>
      </div>
      <div>
        <Result employeeId={values?.employeeId} />
      </div>
    </div>
  );
};

export default SalaryStatementFields;
