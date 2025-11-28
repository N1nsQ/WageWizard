import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import { API_BASE } from "../../config";
import type { BackendError } from "../../models/BackendError";
import type { EmployeesSummaryDto } from "../../models/EmployeesSummaryDto";
import type { EmployeeDto } from "../../models/EmployeeDto";
import type { NewEmployeeRequest } from "../../models/NewEmployeeRequest";
import type { RootState } from "../store";

interface EmployeesState {
  employees: EmployeesSummaryDto[];
  selectedEmployee: EmployeeDto | null;

  loadingSummary: boolean;
  loadingDetails: boolean;
  creating: boolean;

  error: string | null;
}

const initialState: EmployeesState = {
  employees: [],
  selectedEmployee: null,

  loadingSummary: false,
  loadingDetails: false,
  creating: false,

  error: null,
};

interface ThunkConfig {
  rejectValue: BackendError;
}

// ==========================================================
// Async Thunks
// ==========================================================

// Fetch single employee by id
export const fetchEmployeeById = createAsyncThunk<
  EmployeeDto,
  string,
  ThunkConfig
>("employees/fetchById", async (id, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_BASE}/api/Employees/${id}`);
    if (!response.ok) {
      return rejectWithValue(await response.json());
    }
    return await response.json();
  } catch {
    return rejectWithValue({ message: "error_messages.server_unreachable" });
  }
});

// Fetch all employees (summary list)
export const fetchEmployeesSummary = createAsyncThunk<
  EmployeesSummaryDto[],
  void,
  ThunkConfig
>("employees/fetchSummary", async (_, { rejectWithValue }) => {
  try {
    const response = await fetch(`${API_BASE}/api/Employees/summary`);
    if (!response.ok) {
      return rejectWithValue(await response.json());
    }

    return await response.json();
  } catch {
    return rejectWithValue({ message: "error_messages.server_unreachable" });
  }
});

// Create new employee
export const createEmployee = createAsyncThunk<
  EmployeeDto,
  NewEmployeeRequest,
  ThunkConfig
>("employees/create", async (employee, { rejectWithValue }) => {
  try {
    console.log("Sending POST request", employee);

    const response = await fetch(`${API_BASE}/api/Employees`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(employee),
    });

    console.log("Response OK:", response.ok);
    console.log("HTTP status:", response.status);
    console.log("Response URL:", response.url);
    console.log("Received response", response.status, response.statusText);

    if (!response.ok) {
      let errorBody: BackendError = {
        message: "backend_error_messages.unknown_error",
      };

      try {
        const data = await response.json();
        console.log("Backend response JSON:", data);
        if (data?.message) errorBody = { message: data.message };
        else if (data?.code) errorBody = { message: data.code };
      } catch (jsonErr) {
        console.warn("Backend ei palauttanut JSONia", jsonErr);
      }

      console.log("Rejecting with errorBody:", errorBody);
      return rejectWithValue(
        errorBody ?? { message: "error_messages.unknown_server_error" }
      );
    }

    const result = (await response.json()) as EmployeeDto;
    console.log("Create employee result:", result);
    return result;
  } catch (err) {
    console.error("Fetch failed:", err);
    return rejectWithValue({ message: "error_messages.server_unreachable" });
  }
});

// ==========================================================
// Slice
// ==========================================================

export const employeesSlice = createSlice({
  name: "employees",
  initialState,
  reducers: {
    clearEmployeeError(state) {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    // ----- Fetch by ID -----
    builder
      .addCase(fetchEmployeeById.pending, (state) => {
        state.loadingDetails = true;
        state.error = null;
      })
      .addCase(
        fetchEmployeeById.fulfilled,
        (state, action: PayloadAction<EmployeeDto>) => {
          state.loadingDetails = false;
          state.selectedEmployee = action.payload;
        }
      )
      .addCase(
        fetchEmployeeById.rejected,
        (state, action: PayloadAction<BackendError | undefined>) => {
          state.loadingDetails = false;
          state.error =
            action.payload?.message ?? "error_messages.unknown_server_error";
        }
      );

    // ----- Fetch summary -----
    builder
      .addCase(fetchEmployeesSummary.pending, (state) => {
        state.loadingSummary = true;
        state.error = null;
      })
      .addCase(
        fetchEmployeesSummary.fulfilled,
        (state, action: PayloadAction<EmployeesSummaryDto[]>) => {
          state.loadingSummary = false;
          state.employees = action.payload;
        }
      )
      .addCase(
        fetchEmployeesSummary.rejected,
        (state, action: PayloadAction<BackendError | undefined>) => {
          state.loadingSummary = false;
          state.error =
            action.payload?.message ?? "error_messages.unknown_server_error";
        }
      );

    // ----- Create employee -----
    builder
      .addCase(createEmployee.pending, (state) => {
        state.creating = true;
        state.error = null;
      })
      .addCase(
        createEmployee.fulfilled,
        (state, action: PayloadAction<EmployeeDto>) => {
          state.creating = false;
          state.error = null;

          state.employees.push({
            id: action.payload.id,
            firstName: action.payload.firstName,
            lastName: action.payload.lastName,
            jobTitle: action.payload.jobTitle,
            email: action.payload.email,
            imageUrl: action.payload.imageUrl,
          });
        }
      )
      .addCase(
        createEmployee.rejected,
        (state, action: PayloadAction<BackendError | undefined>) => {
          console.log("Reducer rejected:", action.payload);
          state.creating = false;
          state.error =
            action.payload?.message ?? "error_messages.unknown_server_error";
        }
      );
  },
});

// ==========================================================
// Selectors
// ==========================================================

export const selectEmployeesSummary = (state: RootState) =>
  state.employees.employees;

export const selectSelectedEmployee = (state: RootState) =>
  state.employees.selectedEmployee;

export const selectEmployeesError = (state: RootState) => state.employees.error;

export const selectLoadingEmployees = (state: RootState) =>
  state.employees.loadingSummary;

export const selectLoadingEmployeeDetails = (state: RootState) =>
  state.employees.loadingDetails;

export const selectCreatingEmployee = (state: RootState) =>
  state.employees.creating;

export const { clearEmployeeError } = employeesSlice.actions;

export default employeesSlice.reducer;
