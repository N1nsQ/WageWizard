import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import employeesSummaryReducer from "./slices/EmployeesSlice";
import employeeDetailsReducer from "./slices/EmployeeDetailsSlice";
import employeesSalaryDetailsReducer from "./slices/employeesSalaryDetailsSlice";
import salaryStatementCalculationsReducer from "./slices/SalaryStatementSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    employeesSummary: employeesSummaryReducer,
    employeeDetails: employeeDetailsReducer,
    employeesSalaryDetails: employeesSalaryDetailsReducer,
    salaryStatementCalculation: salaryStatementCalculationsReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
