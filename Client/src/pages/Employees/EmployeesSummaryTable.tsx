import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import { useTranslation } from "react-i18next";
import type { EmployeesSummary } from "../../models/EmployeesSummary";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { fetchEmployeesSummary } from "../../redux/slices/EmployeesSlice";
import { type AppDispatch, type RootState } from "../../redux/store";
import OpenInNewIcon from "@mui/icons-material/OpenInNew";

const EmployeesSummaryTable = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();
  const {
    data: employees,
    isLoading,
    error,
  } = useSelector((state: RootState) => state.employeesSummary);

  useEffect(() => {
    dispatch(fetchEmployeesSummary());
  }, [dispatch]);

  const API_BASE = "https://localhost:7032";

  const columns: GridColDef<EmployeesSummary>[] = [
    {
      field: "imageUrl",
      headerName: t("employees.image"),
      headerAlign: "center",
      align: "center",
      flex: 0.3,
      renderCell: (params) => {
        console.log("Image URL:", params.value);
        const src = params.value
          ? `${API_BASE}${params.value}`
          : "/default.png";
        return (
          <div className="employee-image-cell">
            <img
              src={src}
              alt="employee"
              style={{ width: 40, height: 40, borderRadius: "50%" }}
            />
          </div>
        );
      },
    },
    {
      field: "firstName",
      headerName: t("employees.firstname"),
      flex: 0.7,
      editable: true,
    },
    {
      field: "lastName",
      headerName: t("employees.lastname"),
      flex: 0.7,
      editable: true,
    },
    {
      field: "jobTitle",
      headerName: t("employees.jobtitle"),
      flex: 0.7,
      editable: true,
    },
    {
      field: "email",
      headerName: t("employees.email"),
      flex: 1,
      editable: true,
    },
    {
      field: "link",
      headerName: t("employees.open_details"),
      headerAlign: "center",
      align: "center",
      flex: 0.3,
      renderCell: () => {
        //const src = params.value
        return (
          <div>
            <OpenInNewIcon />
          </div>
        );
      },
    },
  ];

  console.log("Employees: ", employees);
  return (
    <div>
      {isLoading && <p>{t("loading")}</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}

      <Box className="employees-table-container">
        <DataGrid
          autoHeight
          rows={employees}
          getRowId={(row) => row.email}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { pageSize: 10 },
            },
          }}
          pageSizeOptions={[10]}
          disableRowSelectionOnClick
        />
      </Box>
    </div>
  );
};

export default EmployeesSummaryTable;
