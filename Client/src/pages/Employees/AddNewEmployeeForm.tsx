import { Form } from "react-final-form";
import AddNewEmployeeFields from "./AddNewEmployeeFields";
import DatePickerLocalizationProvider from "../../common/DatePickerLocalizationProvider";

const AddNewEmployeeForm = () => {
  const onSubmit = () => console.log("Submit");

  return (
    <Form
      onSubmit={onSubmit}
      render={({ handleSubmit }) => (
        <form onSubmit={handleSubmit}>
          <DatePickerLocalizationProvider>
            <AddNewEmployeeFields />
          </DatePickerLocalizationProvider>
        </form>
      )}
    />
  );
};

export default AddNewEmployeeForm;
