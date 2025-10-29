import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { EmployeesSalaryDetails } from "../../models/EmployeesSalaryDetails";
import type { ErrorMessage } from "../../models/ErrorMessage";
import { API_BASE } from "../../config";

interface EmployeesSalaryDetailsState {
  data: EmployeesSalaryDetails[] | null;
  isLoading: boolean;
  error: string | null;
}

const initialState: EmployeesSalaryDetailsState = {
  data: null,
  isLoading: false,
  error: null,
};

const fetchEmployeesSalaryDetails = createAsyncThunk<
  EmployeesSalaryDetails[],
  void,
  { rejectValue: ErrorMessage }
>("employees/fetchSalaryDetails", async (_, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_BASE}/api/Employees/paymentDetails`);

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

const employeesSalaryDetailsSlice = createSlice({
  name: "employeesSalaryDetails",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchEmployeesSalaryDetails.pending, (state) => {
      state.isLoading = true;
    });
    builder.addCase(fetchEmployeesSalaryDetails.fulfilled, (state, action) => {
      state.isLoading = false;
      state.data = action.payload;
    });
    builder.addCase(fetchEmployeesSalaryDetails.rejected, (state, action) => {
      state.isLoading = false;
      state.error =
        action.payload?.code ?? "backend_error_messages.employees_not_found";
    });
  },
});

export default employeesSalaryDetailsSlice.reducer;
export { fetchEmployeesSalaryDetails };
