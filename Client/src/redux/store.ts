import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import employeesSummaryReducer from "./slices/EmployeesSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    employeesSummary: employeesSummaryReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
