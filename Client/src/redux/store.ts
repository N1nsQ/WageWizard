import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import employeesSalaryDetailsReducer from "./slices/employeesSalaryDetailsSlice";
import salaryStatementCalculationsReducer from "./slices/SalaryStatementSlice";
import employeesReducer from "./slices/EmployeesSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    employeesSalaryDetails: employeesSalaryDetailsReducer,
    salaryStatementCalculation: salaryStatementCalculationsReducer,
    employees: employeesReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
