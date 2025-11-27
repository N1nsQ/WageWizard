import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { NewEmployeeRequest } from "../../models/NewEmployeeRequest";
import { API_BASE } from "../../config";

interface EmployeeCreateState {
  loading: boolean;
  error: string | null;
  success: boolean;
}

const initialState: EmployeeCreateState = {
  loading: false,
  error: null,
  success: false,
};

export const createEmployee = createAsyncThunk(
  "employee/create",
  async (employee: NewEmployeeRequest, { rejectWithValue }) => {
    try {
      const response = await fetch(`${API_BASE}/api/Employees`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(employee),
      });
      if (!response.ok) {
        const data = await response.json();
        return rejectWithValue(
          data.code ?? "backend_error_messages.unknown_error"
        );
      }
      return await response.json();
    } catch {
      return rejectWithValue({
        code: "backend_error_messages.server_unreachable",
      });
    }
  }
);

const employeeCreateSlice = createSlice({
  name: "employeeCreate",
  initialState,
  reducers: {
    reset: (state) => {
      state.loading = false;
      state.error = null;
      state.success = false;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(createEmployee.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(createEmployee.fulfilled, (state) => {
        state.loading = false;
        state.success = true;
      })
      .addCase(createEmployee.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.success = false;
      });
  },
});

export const { reset } = employeeCreateSlice.actions;
export default employeeCreateSlice.reducer;
