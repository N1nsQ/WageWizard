import { render, screen, waitFor } from "@testing-library/react";
import { describe, it, expect, vi, beforeEach } from "vitest";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../../../redux/slices/authSlice";
import Login from "../Login";
import userEvent from "@testing-library/user-event";

// mockataan i18next-kirjaston käyttö
vi.mock("react-i18next", () => ({
  useTranslation: () => ({ t: (key: string) => key }),
}));

const renderWithProviders = (ui: React.ReactNode, preloadedState = {}) => {
  const store = configureStore({
    reducer: { auth: authReducer },
    preloadedState,
  });

  return render(
    <Provider store={store}>
      <MemoryRouter>{ui}</MemoryRouter>
    </Provider>
  );
};

// Otsikko renderöityy oikein
describe("Login page", () => {
  it("renders the login title", () => {
    renderWithProviders(<Login />);
    expect(screen.getByTestId("login-title")).toBeInTheDocument();
  });

  // Kirjaudu-painike löytyy
  it("rendersthe login button", () => {
    renderWithProviders(<Login />);
    expect(screen.getByTestId("login-submit")).toBeInTheDocument();
  });

  // Käyttäjätunnus ja salasana kentät löytyvät
  it("renders username and password fields", () => {
    renderWithProviders(<Login />);
    expect(screen.getByTestId("username-field")).toBeInTheDocument();
    expect(screen.getByTestId("password-field")).toBeInTheDocument();
  });

  // Kenttiin voi kirjoittaa
  it("allows typing into username and password fields", async () => {
    renderWithProviders(<Login />);
    const usernameField = screen
      .getByTestId("username-field")
      .querySelector("input") as HTMLInputElement;
    const passwordField = screen
      .getByTestId("password-field")
      .querySelector("input") as HTMLInputElement;

    await userEvent.type(usernameField, "testUser");
    await userEvent.type(passwordField, "password123");

    expect(usernameField).toHaveValue("testUser");
    expect(passwordField).toHaveValue("password123");
  });

  // Virheviesti näkyy, jos Reduxin tila palauttaa errorin
  it("shows error message if login fails", () => {
    const preloadedState = {
      auth: {
        isAuthenticated: false,
        user: null,
        error: "backend_error_messages.invalid_username",
        isLoading: false,
      },
    };
    renderWithProviders(<Login />, preloadedState);
    expect(screen.getByTestId("login-error-message")).toBeInTheDocument();
  });
});

describe("Login page should show loading spinner", () => {
  beforeEach(() => {
    vi.restoreAllMocks();
  });

  it("shows spinner while loginUser thunk is pending", async () => {
    // mockataan fetch niin että se resolvaa viiveen jälkeen
    vi.spyOn(global, "fetch").mockImplementation(
      () =>
        new Promise((resolve) =>
          setTimeout(
            () =>
              resolve({
                ok: true,
                json: async () => ({
                  userId: "123",
                  username: "TestUser",
                  role: 1,
                }),
              } as Response),
            200
          )
        )
    );

    renderWithProviders(<Login />);

    const usernameInput = screen.getByTestId("username-field");
    const passwordInput = screen.getByTestId("password-field");
    const loginButton = screen.getByTestId("login-submit");

    await userEvent.type(usernameInput, "TestUser");
    await userEvent.type(passwordInput, "password123");
    await userEvent.click(loginButton);

    // spinnerin pitäisi näkyä heti dispatchin jälkeen
    expect(screen.getByRole("progressbar")).toBeInTheDocument();

    // spinner katoaa, kun mockattu fetch palauttaa datan
    await waitFor(
      () => {
        expect(screen.queryByRole("progressbar")).not.toBeInTheDocument();
      },
      { timeout: 1000 }
    );
  });
});
