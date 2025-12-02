import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { SalaryStatementCalculationDto } from "../../models/SalaryStatementCalculationDto";
import { API_BASE } from "../../config";
import type { EmployeeLookupDto } from "../../models/EmployeeLookupDto";
import type { BackendError } from "../../models/BackendError";

interface SalaryStatementState {
  lookupEmployees: EmployeeLookupDto[];
  selectedEmployeeSalaryDetails: SalaryStatementCalculationDto | null;
  loadingEmployees: boolean;
  loadingDetails: boolean;
  error: string | null;
}

const initialState: SalaryStatementState = {
  lookupEmployees: [],
  selectedEmployeeSalaryDetails: null,
  loadingDetails: false,
  loadingEmployees: false,
  error: null,
};

interface ThunkConfig {
  rejectValue: BackendError;
}

const fetchEmployeesLookup = createAsyncThunk<
  EmployeeLookupDto[],
  void,
  ThunkConfig
>("salaryStatement/fetchLookup", async (_, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_BASE}/api/employees/lookup`);
    if (!response.ok) return rejectWithValue(await response.json());
    return await response.json();
  } catch {
    return rejectWithValue({ message: "error_messages.server_unreachable" });
  }
});

const fetchSalaryStatementCalculations = createAsyncThunk<
  SalaryStatementCalculationDto,
  string,
  ThunkConfig
>("payroll/salaryStatementCalculation", async (id, { rejectWithValue }) => {
  try {
    const response = await fetch(
      `${API_BASE}/api/Payrolls/SalaryStatement/${id}`
    );

    if (!response.ok) {
      return rejectWithValue(await response.json());
    }

    return await response.json();
  } catch {
    return rejectWithValue({ message: "error_messages.server_unreachable" });
  }
});

const salaryStatementSlice = createSlice({
  name: "salaryStatement",
  initialState,
  reducers: {
    clearSalaryStatementError(state) {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    /* ----- Lookup ----- */
    builder.addCase(fetchEmployeesLookup.pending, (state) => {
      state.loadingEmployees = true;
      state.error = null;
    });

    builder.addCase(fetchEmployeesLookup.fulfilled, (state, action) => {
      state.loadingEmployees = false;
      state.lookupEmployees = action.payload;
    });

    builder.addCase(fetchEmployeesLookup.rejected, (state, action) => {
      state.loadingEmployees = false;
      state.error =
        action.payload?.message ?? "error_messages.unknown_server_error";
    });

    /* ----- Salary details ----- */
    builder.addCase(fetchSalaryStatementCalculations.pending, (state) => {
      state.loadingDetails = true;
      state.error = null;
    });

    builder.addCase(
      fetchSalaryStatementCalculations.fulfilled,
      (state, action) => {
        state.loadingDetails = false;
        state.selectedEmployeeSalaryDetails = action.payload;
      }
    );

    builder.addCase(
      fetchSalaryStatementCalculations.rejected,
      (state, action) => {
        state.loadingDetails = false;
        state.error =
          action.payload?.message ?? "error_messages.unknown_server_error";
      }
    );
  },
});

export const { clearSalaryStatementError } = salaryStatementSlice.actions;
export default salaryStatementSlice.reducer;
export { fetchEmployeesLookup };
export { fetchSalaryStatementCalculations };
