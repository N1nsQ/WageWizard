import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import employeesReducer from "./slices/EmployeesSlice";
import salaryStatementReducer from "./slices/SalaryStatementSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    employees: employeesReducer,
    salaryStatement: salaryStatementReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
