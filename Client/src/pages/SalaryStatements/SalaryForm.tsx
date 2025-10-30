import { useTranslation } from "react-i18next";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/store";
import { useEffect } from "react";
import { fetchEmployeesSalaryDetails } from "../../redux/slices/employeesSalaryDetailsSlice";
import { Form } from "react-final-form";
import SalaryStatementFields from "./SalaryStatementFields";

const SalaryForm = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();

  const { data: employees } = useSelector(
    (state: RootState) => state.employeesSalaryDetails
  );

  console.log("Työntekijät: ", employees);

  useEffect(() => {
    dispatch(fetchEmployeesSalaryDetails());
  }, [dispatch]);

  const onSubmit = () => {
    console.log("Lomaketta klikattu");
  };

  return (
    <div>
      <Form
        onSubmit={onSubmit}
        render={({ handleSubmit }) => (
          <form onSubmit={handleSubmit}>
            <SalaryStatementFields />
          </form>
        )}
      />
    </div>
  );
};

export default SalaryForm;
