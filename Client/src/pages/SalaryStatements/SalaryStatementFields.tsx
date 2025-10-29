import { Grid, MenuItem, TextField } from "@mui/material";
import { Field, useForm, useFormState } from "react-final-form";
import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store";
import { useTranslation } from "react-i18next";
import { useEffect } from "react";

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
      form.change("veroprosentti", selectedEmployee.taxPercentage);
      form.change("peruspalkka", selectedEmployee.salaryAmount);
      form.change("age", selectedEmployee.age);
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
      </Grid>
    </div>
  );
};

export default SalaryStatementFields;
