import { Form } from "react-final-form";
import AddNewEmployeeFields from "./AddNewEmployeeFields";
import DatePickerLocalizationProvider from "../../common/DatePickerLocalizationProvider";
import { createEmployee } from "../../redux/slices/EmployeesSlice";
import type { NewEmployeeRequest } from "../../models/NewEmployeeRequest";
import { useAppDispatch } from "../../hooks/hooks";
import { fetchEmployeesSummary } from "../../redux/slices/EmployeesSlice";

interface Props {
  onClose: () => void;
  onSubmitRef?: (handleSubmit: () => void) => void;
}

const AddNewEmployeeForm = ({ onClose, onSubmitRef }: Props) => {
  const dispatch = useAppDispatch();

  const onSubmit = async (values: NewEmployeeRequest) => {
    const payload = {
      firstName: values.firstName,
      lastName: values.lastName,
      jobTitle: values.jobTitle,
      email: values.email,
      homeAddress: values.homeAddress,
      postalCode: values.postalCode,
      city: values.city,
      bankAccountNumber: values.bankAccountNumber,
      taxRate: Number(values.taxRate),
      monthlySalary: values.monthlySalary ?? 0,
      startDate: values.startDate,
      dateOfBirth: values.dateOfBirth,
    };

    const resultAction = await dispatch(createEmployee(payload));

    if (createEmployee.fulfilled.match(resultAction)) {
      dispatch(fetchEmployeesSummary());
      onClose();
    } else if (createEmployee.rejected.match(resultAction)) {
      console.error("Error adding employee:", resultAction.payload);
    }
  };

  return (
    <div>
      <Form
        onSubmit={onSubmit}
        render={({ handleSubmit }) => {
          onSubmitRef?.(handleSubmit);

          return (
            <form onSubmit={handleSubmit}>
              <DatePickerLocalizationProvider>
                <AddNewEmployeeFields />
              </DatePickerLocalizationProvider>
            </form>
          );
        }}
      />
    </div>
  );
};

export default AddNewEmployeeForm;
