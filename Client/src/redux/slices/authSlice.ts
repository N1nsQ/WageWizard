import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { LoginDto } from "../../models/LoginDto"; // Tämän mukana tulee käyttäjän syöttämät kirjautumistiedot

// virheviestin rajapinta
interface LoginError {
  code: string; // esim. "backend_error_messages.invalid_username"
  message?: string; // vapaaehtoinen viesti, backend voi antaa
}

// AuthState rajapinta, kuvaa reduxin tilan rakennetta
interface AuthState {
  isAuthenticated: boolean;
  user: string | null;
  error: string | null;
  isLoading: boolean; // loginUser.pending tila, näytetäänkö "latausrinkula"
}

// Alkutila. Oletusarvona kukaan ei ole kirjautunut sisään
const initialState: AuthState = {
  isAuthenticated: false,
  user: null,
  error: null,
  isLoading: false,
};

// Kirjautumisen API-kutsu
export const loginUser = createAsyncThunk<
  // Onnistuneen kirjautumisen tyyppi
  { userId: string; username: string; role: number },
  // Argument type
  LoginDto,
  // ThunkAPI type
  { rejectValue: LoginError }
>("auth/loginUser", async (loginData: LoginDto, thunkAPI) => {
  try {
    const response = await fetch("https://localhost:7032/api/Login/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(loginData),
    });

    if (!response.ok) {
      const data: LoginError = await response.json();
      return thunkAPI.rejectWithValue(data);
    }

    return await response.json();
  } catch {
    return thunkAPI.rejectWithValue({
      code: "backend_error_messages.server_unreachable",
      message: "Could not connect to server",
    });
  }
});

// Redux Toolkit luo prefixistä kolme eri actionia:
// auth/loginUser/pending --> lähetetään heti kun thunk käynnistyy (voidaan näyttää esim Ladataan spinneri)
// auth/loginUser/fulfilled --> lähetetään, jos async-funktio päättyy onnistuneesti (return)
// auth/loginUser/rejected --> lähetetään, jos funktio päättyy virheeseen tai thunkAPI.rejectedWithValue

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {}, // reducers tyhjä, ei synkronisia reducereita -->
  // kaikki logiikka menee extraReducers:in kautta
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        //loginUser on createAsyncThunkin luoma thunk.
        state.isLoading = false;
        state.isAuthenticated = true;
        state.user = action.payload.username;
        state.error = null;
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.isLoading = false;
        state.isAuthenticated = false;
        state.user = null;
        state.error =
          action.payload?.code ?? "backend_error_messages.invalid_username";
      });
  },
});

export default authSlice.reducer;
