import { configureStore } from "@reduxjs/toolkit";
import employeesSummaryReducer from "../../redux/slices/EmployeesSlice";
import employeeDetailsReducer from "../../redux/slices/EmployeeDetailsSlice";
import authReducer from "../../redux/slices/authSlice";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { render } from "@testing-library/react";

export const renderWithProviders = (
  ui: React.ReactNode,
  preloadedState = {}
) => {
  const store = configureStore({
    reducer: {
      auth: authReducer,
      employeesSummary: employeesSummaryReducer,
      employeeDetails: employeeDetailsReducer,
    },
    preloadedState,
  });

  return render(
    <Provider store={store}>
      <MemoryRouter>{ui}</MemoryRouter>
    </Provider>
  );
};
