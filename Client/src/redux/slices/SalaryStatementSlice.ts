import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { SalaryStatementCalculationDto } from "../../models/SalaryStatementCalculationDto";
import type { ErrorMessage } from "../../models/ErrorMessage";
import { API_BASE } from "../../config";

interface SalaryStatementState {
  data: SalaryStatementCalculationDto | null;
  isLoading: boolean;
  error: string | null;
}

const initialState: SalaryStatementState = {
  data: null,
  isLoading: false,
  error: null,
};

const fetchSalaryStatementCalculations = createAsyncThunk<
  SalaryStatementCalculationDto,
  string,
  { rejectValue: ErrorMessage }
>("payroll/salaryStatementCalculation", async (id, { rejectWithValue }) => {
  try {
    const response = await fetch(
      `${API_BASE}/api/Payrolls/SalaryStatement?employeeId=${id}`
    );
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

const salaryStatementCalculationSlice = createSlice({
  name: "salaryStatementCalculation",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchSalaryStatementCalculations.pending, (state) => {
      state.isLoading = true;
      state.error = null;
    });
    builder.addCase(
      fetchSalaryStatementCalculations.fulfilled,
      (state, action) => {
        state.isLoading = false;
        state.data = action.payload;
      }
    );
    builder.addCase(
      fetchSalaryStatementCalculations.rejected,
      (state, action) => {
        state.isLoading = false;
        state.error =
          action.payload?.code ?? "backend_error_messages.employee_not_found";
      }
    );
  },
});

export default salaryStatementCalculationSlice.reducer;
export { fetchSalaryStatementCalculations };
