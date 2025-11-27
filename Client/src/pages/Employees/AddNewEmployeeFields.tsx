import { Box, TextField } from "@mui/material";
import { Field } from "react-final-form";
import { useTranslation } from "react-i18next";
import { DatePicker } from "@mui/x-date-pickers";

const AddNewEmployeeFields = () => {
  const { t } = useTranslation();

  return (
    <div>
      <div>
        <Box
          className="center-and-shrink"
          component="img"
          src="/default.png"
          alt={t("employees.employeeImg")}
        />
      </div>
      <div style={{ display: "flex", gap: "16px" }}>
        <div style={{ flex: 1 }}>
          <Field name="firstName">
            {({ input, meta }) => (
              <TextField
                {...input}
                label={t("employees.firstname")}
                margin="normal"
                error={meta.touched && meta.error ? true : false}
                fullWidth
              />
            )}
          </Field>
        </div>
        <div style={{ flex: 1 }}>
          <Field name="lastName">
            {({ input, meta }) => (
              <TextField
                {...input}
                label={t("employees.lastname")}
                margin="normal"
                error={meta.touched && meta.error ? true : false}
                fullWidth
              />
            )}
          </Field>
        </div>
      </div>
      <div>
        <Field name="jobTitle">
          {({ input, meta }) => (
            <TextField
              {...input}
              label={t("employees.jobtitle")}
              margin="normal"
              error={meta.touched && meta.error ? true : false}
              fullWidth
            />
          )}
        </Field>
      </div>
      <div>
        <Field name="email">
          {({ input, meta }) => (
            <TextField
              {...input}
              label={t("employees.email")}
              margin="normal"
              error={meta.touched && meta.error ? true : false}
              fullWidth
            />
          )}
        </Field>
      </div>
      <div>
        <Field name="homeAddress">
          {({ input, meta }) => (
            <TextField
              {...input}
              label={t("employees.homeAddress")}
              margin="normal"
              error={meta.touched && meta.error ? true : false}
              fullWidth
            />
          )}
        </Field>
      </div>
      <div style={{ display: "flex", gap: "16px" }}>
        <div style={{ flex: 1 }}>
          <Field name="postalCode">
            {({ input, meta }) => (
              <TextField
                {...input}
                label={t("employees.postal_code")}
                margin="normal"
                error={meta.touched && meta.error ? true : false}
                fullWidth
              />
            )}
          </Field>
        </div>
        <div style={{ flex: 1 }}>
          <Field name="city">
            {({ input, meta }) => (
              <TextField
                {...input}
                label={t("employees.city")}
                margin="normal"
                error={meta.touched && meta.error ? true : false}
                fullWidth
              />
            )}
          </Field>
        </div>
      </div>
      <div>
        <Field name="bankAccountNumber">
          {({ input, meta }) => (
            <TextField
              {...input}
              label={t("employees.bankAccountNumber")}
              margin="normal"
              error={meta.touched && meta.error ? true : false}
              fullWidth
            />
          )}
        </Field>
      </div>
      <div style={{ display: "flex", gap: "16px" }}>
        <div style={{ flex: 1 }}>
          <Field name="taxRate">
            {({ input, meta }) => (
              <TextField
                {...input}
                label={t("employees.taxPercentage")}
                margin="normal"
                type="number"
                error={meta.touched && meta.error ? true : false}
                fullWidth
              />
            )}
          </Field>
        </div>
        <div style={{ flex: 1 }}>
          <Field name="monthlySalary">
            {({ input, meta }) => (
              <TextField
                {...input}
                label={t("employees.salaryAmount")}
                margin="normal"
                type="number"
                value={input.value ?? ""}
                onChange={(e) =>
                  input.onChange(
                    e.target.value !== "" ? Number(e.target.value) : 0
                  )
                }
                error={meta.touched && meta.error ? true : false}
                fullWidth
              />
            )}
          </Field>
        </div>
      </div>
      <div style={{ display: "flex", gap: "16px" }}>
        <div style={{ flex: 1 }}>
          <Field name="startDate">
            {({ input, meta }) => (
              <DatePicker
                label={t("employees.startDate")}
                value={input.value || null}
                onChange={(date) => input.onChange(date)}
                slotProps={{
                  textField: {
                    margin: "normal",
                    error: meta.touched && !!meta.error,
                    fullWidth: true,
                  },
                }}
              />
            )}
          </Field>
        </div>
        <div style={{ flex: 1 }}>
          <Field name="dateOfBirth">
            {({ input, meta }) => (
              <DatePicker
                label={t("employees.dateOfBirth")}
                value={input.value || null}
                onChange={(date) => input.onChange(date)}
                slotProps={{
                  textField: {
                    margin: "normal",
                    error: meta.touched && !!meta.error,
                    fullWidth: true,
                  },
                }}
              />
            )}
          </Field>
        </div>
      </div>
    </div>
  );
};

export default AddNewEmployeeFields;
