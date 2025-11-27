import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { EmployeeDetails } from "../../models/EmployeeDetails";
import type { ErrorMessage } from "../../models/ErrorMessage";
import { API_BASE } from "../../config";

interface EmployeeDetailsState {
  data: EmployeeDetails | null;
  isLoading: boolean;
  error: string | null;
}

const initialState: EmployeeDetailsState = {
  data: null,
  isLoading: false,
  error: null,
};

const fetchEmployeeDetails = createAsyncThunk<
  EmployeeDetails,
  string,
  { rejectValue: ErrorMessage }
>("employees/fetchDetails", async (id, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_BASE}/api/Employees/${id}`);
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

const employeeDetailsSlice = createSlice({
  name: "EmployeeDetails",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchEmployeeDetails.pending, (state) => {
      state.isLoading = true;
    });
    builder.addCase(fetchEmployeeDetails.fulfilled, (state, action) => {
      state.isLoading = false;
      state.data = action.payload;
    });
    builder.addCase(fetchEmployeeDetails.rejected, (state, action) => {
      state.isLoading = false;
      state.error =
        action.payload?.code ?? "backend_error_messages.employee_not_found";
    });
  },
});

export default employeeDetailsSlice.reducer;
export { fetchEmployeeDetails };
