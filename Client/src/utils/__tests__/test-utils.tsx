import { configureStore } from "@reduxjs/toolkit";
import employeesSummaryReducer from "../../redux/slices/EmployeesSlice";
import authReducer from "../../redux/slices/authSlice";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import employeesReducer from "../../redux/slices/EmployeesSlice";

export const renderWithProviders = (
  ui: React.ReactNode,
  preloadedState = {}
) => {
  const store = configureStore({
    reducer: {
      auth: authReducer,
      employeesSummary: employeesSummaryReducer,
      employees: employeesReducer,
    },
    preloadedState,
  });

  return render(
    <Provider store={store}>
      <MemoryRouter>{ui}</MemoryRouter>
    </Provider>
  );
};
