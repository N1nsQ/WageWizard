import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../redux/store";
import { useEffect } from "react";
import { fetchEmployeesSalaryDetails } from "../../redux/slices/employeesSalaryDetailsSlice";
import { Form } from "react-final-form";
import SalaryStatementFields from "./SalaryStatementFields";

const SalaryForm = () => {
  const dispatch = useDispatch<AppDispatch>();

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
