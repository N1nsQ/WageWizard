import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { LoginDto } from "../../models/LoginDto";
import type { ErrorMessage } from "../../models/ErrorMessage";
import { API_BASE } from "../../config";
import type { AuthState } from "../../models/AuthState";
import type { RootState } from "../store";

interface LoginResponse {
  token: string;
  userId: string;
  username: string;
  role: string;
}

const initialState: AuthState = {
  token: localStorage.getItem("token"),
  userId: localStorage.getItem("userId"),
  userName: localStorage.getItem("username"),
  role: localStorage.getItem("role"),
  error: null,
  isLoading: false,
  isAuthenticated: !!localStorage.getItem("token"),
};

export const loginUser = createAsyncThunk<
  LoginResponse,
  LoginDto,
  { rejectValue: ErrorMessage }
>("auth/loginUser", async (loginData: LoginDto, thunkAPI) => {
  try {
    const response = await fetch(`${API_BASE}/api/Login/`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(loginData),
    });

    if (!response.ok) {
      const data: ErrorMessage = await response.json();
      return thunkAPI.rejectWithValue(data);
    }

    const result: LoginResponse = await response.json();

    localStorage.setItem("token", result.token);
    localStorage.setItem("userId", result.userId);
    localStorage.setItem("username", result.username);
    localStorage.setItem("role", result.role);

    return result;
  } catch {
    return thunkAPI.rejectWithValue({
      code: "backend_error_messages.server_unreachable",
    });
  }
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout(state) {
      state.token = null;
      state.userName = null;
      state.role = null;
      state.userId = null;
      state.isAuthenticated = false;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isAuthenticated = true;
        state.token = action.payload.token;
        state.userId = action.payload.userId;
        state.userName = action.payload.username;
        state.role = action.payload.role;
        state.error = null;

        console.log("action.payload.token", action.payload.token);
        console.log("action.payload.uresId", action.payload.userId);
        console.log("action.payload.role", action.payload.role);
      })

      .addCase(loginUser.rejected, (state, action) => {
        state.isLoading = false;
        state.isAuthenticated = false;
        state.userId = null;
        state.token = null;
        state.error =
          action.payload?.code ?? "backend_error_messages.invalid_username";
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;

export const selectUserId = (state: RootState) => state.auth.userId;
export const selectUserName = (state: RootState) => state.auth.userName;
export const selectUserRole = (state: RootState) => state.auth.role;
