import { describe, it, expect } from "vitest";
import { screen } from "@testing-library/react";
import { renderWithProviders } from "../../../utils/__tests__/test-utils";
import EmployeesSummaryTable from "../EmployeesSummaryTable";

describe("EmployeesSummaryTable columns", () => {
  it("renders all column headers", () => {
    renderWithProviders(<EmployeesSummaryTable />);

    expect(screen.getByText("employees.image")).toBeInTheDocument();
    expect(screen.getByText("employees.firstname")).toBeInTheDocument();
    expect(screen.getByText("employees.lastname")).toBeInTheDocument();
    expect(screen.getByText("employees.jobtitle")).toBeInTheDocument();
    expect(screen.getByText("employees.email")).toBeInTheDocument();
    expect(screen.getByText("employees.open_details")).toBeInTheDocument();
  });
});

describe("EmployeesSummaryTable links", () => {
  it("navigates to correct employee details page", () => {
    const preloadedState = {
      employeesSummary: {
        data: [
          {
            id: "123",
            firstName: "Mikki",
            lastName: "Hiiri",
            jobTitle: "Supersankari",
            email: "mikki.hiiri@dreamwork.com",
          },
        ],
        isLoading: false,
        error: null,
      },
    };

    renderWithProviders(<EmployeesSummaryTable />, { preloadedState });

    // Etsi linkki riviltä käyttäen aria-labelia
    const link = screen.getByTestId("employee-link"); // <-- tämä vastaa IconLinkCellin label-propsia

    // Tarkista href
    expect(link).toHaveAttribute("href", "/employees/123");
  });
});
