import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { LoginDto } from "../../models/LoginDto"; // Tämän mukana tulee käyttäjän syöttämät kirjautumistiedot

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
export const loginUser = createAsyncThunk(
  // auth --> kertoo, että tämä kuuluu authSlice -osiolle
  // loginUser --> kertoo mikä toiminto on kyseessä
  "auth/loginUser", // namespace + action name (action type prefix)
  async (loginData: LoginDto, thunkAPI) => {
    try {
      const response = await fetch("https://localhost:7032/api/Login/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(loginData),
      });

      // Jos vastaus ei ole OK, palautetaan virheilmoitus
      if (!response.ok) {
        return thunkAPI.rejectWithValue("Invalid username or password");
      }

      return await response.json();
      // Muu virhe, esim. backend ei vastaa
    } catch {
      return thunkAPI.rejectWithValue("Could not connect to server");
    }
  }
);

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
