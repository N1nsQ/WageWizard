import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import Box from "@mui/material/Box";
import { useTranslation } from "react-i18next";
import type { EmployeesSummaryDto } from "../../models/EmployeesSummaryDto";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { type AppDispatch } from "../../redux/store";
import OpenInNewIcon from "@mui/icons-material/OpenInNew";
import IconLinkCell from "../../common/IconLinkCell";
import LoadingOverlay from "../../common/LoadingOverlay";
import "../../App.css";
import Thumb from "../../common/Thumb";

import {
  fetchEmployeesSummary,
  selectEmployeesSummary,
  selectLoadingEmployees,
  selectEmployeesError,
} from "../../redux/slices/EmployeesSlice";

const EmployeesSummaryTable = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();

  const employees = useSelector(selectEmployeesSummary);
  const loading = useSelector(selectLoadingEmployees);
  const error = useSelector(selectEmployeesError);

  useEffect(() => {
    dispatch(fetchEmployeesSummary());
  }, [dispatch]);

  const columns: GridColDef<EmployeesSummaryDto>[] = [
    {
      field: "imageUrl",
      headerName: t("employees.image"),
      headerAlign: "center",
      align: "center",
      flex: 0.3,
      renderCell: (params) => {
        return (
          <Thumb imageUrl={params.value} alt={t("employees.thumbImage")} />
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
      renderCell: (params) => (
        <IconLinkCell
          to={`/employees/${params.row.id}`}
          icon={<OpenInNewIcon />}
          label={t("employees.link")}
          data-testid={`employee-link-${params.row.id}`}
        />
      ),
    },
  ];

  return (
    <div>
      {loading && <LoadingOverlay isLoading={loading} />}

      {error && (
        <div className="error-message">
          <p style={{ color: "red" }}>{t(error)}</p>
        </div>
      )}

      <Box className="employees-table-container">
        <DataGrid
          rows={employees}
          getRowId={(row) => row.id}
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
