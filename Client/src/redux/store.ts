import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import employeesSummaryReducer from "./slices/EmployeesSlice";
import employeeDetailsReducer from "./slices/EmployeeDetailsSlice";
import employeesSalaryDetailsReducer from "./slices/employeesSalaryDetailsSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    employeesSummary: employeesSummaryReducer,
    employeeDetails: employeeDetailsReducer,
    employeesSalaryDetails: employeesSalaryDetailsReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
