import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { EmployeesSummary } from "../../models/EmployeesSummary";
import type { ErrorMessage } from "../../models/ErrorMessage";
import { API_BASE } from "../../config";

interface EmployeeSummaryState {
  data: EmployeesSummary[];
  isLoading: boolean;
  error: string | null;
}

const initialState: EmployeeSummaryState = {
  data: [],
  isLoading: false,
  error: null,
};

// Api-kutsu
const fetchEmployeesSummary = createAsyncThunk<
  EmployeesSummary[],
  void,
  { rejectValue: ErrorMessage }
>("employees/fetchSummary", async (_, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_BASE}/api/Employees/summary`);
    if (!response.ok) {
      const data: ErrorMessage = await response.json();
      return rejectWithValue(data);
    }
    return await response.json();
  } catch {
    return rejectWithValue({
      code: "backend_error_messages.server_unreachable",
    });
  }
});

// SLICE
const employeesSymmarySlice = createSlice({
  name: "employeesSummary",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchEmployeesSummary.pending, (state) => {
      state.isLoading = true;
    });
    builder.addCase(fetchEmployeesSummary.fulfilled, (state, action) => {
      state.isLoading = false;
      state.data = action.payload;
    });
    builder.addCase(fetchEmployeesSummary.rejected, (state, action) => {
      state.isLoading = false;
      state.error =
        action.payload?.code ?? "backend_error_messages.employees_not_found";
    });
  },
});

export default employeesSymmarySlice.reducer;
export { fetchEmployeesSummary };
