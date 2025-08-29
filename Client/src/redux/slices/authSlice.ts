import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { LoginDto } from "../../models/LoginDto";

interface AuthState {
  isAuthenticated: boolean;
  user: string | null;
  error: string | null;
}

const initialState: AuthState = {
  isAuthenticated: false,
  user: null,
  error: null,
};

export const loginUser = createAsyncThunk(
  "auth/loginUser",
  async (loginData: LoginDto, thunkAPI) => {
    try {
      const response = await fetch("https://localhost:7032/api/Login/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(loginData),
      });

      if (!response.ok) {
        return thunkAPI.rejectWithValue("Invalid username or password");
      }

      return await response.json();
    } catch {
      return thunkAPI.rejectWithValue("Could not connect to server");
    }
  }
);

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.fulfilled, (state, action) => {
        state.isAuthenticated = true;
        state.user = action.payload;
        state.error = null;
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.isAuthenticated = false;
        state.user = null;
        state.error = action.payload as string;
      });
  },
});

export default authSlice.reducer;
