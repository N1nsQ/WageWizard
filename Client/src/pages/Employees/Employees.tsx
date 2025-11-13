import { IconButton, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import EmployeesSummaryTable from "./EmployeesSummaryTable";
import AddCircleOutlineOutlinedIcon from "@mui/icons-material/AddCircleOutlineOutlined";
import AddNewEmployeeCard from "./AddNewEmployeeCard";
import { useState } from "react";

const Employees = () => {
  const { t } = useTranslation();
  const [openDialog, setOpenDialog] = useState(false);

  const handleOpen = () => setOpenDialog(true);
  const handleClose = () => setOpenDialog(false);

  return (
    <div>
      <div className="page-title">
        <Typography
          variant="h4"
          style={{
            textAlign: "center",
          }}
        >
          {t("employees.title")}
        </Typography>

        <div className="add-new-button">
          <IconButton onClick={handleOpen} className="add-new-button-icon">
            <AddCircleOutlineOutlinedIcon sx={{ fontSize: 40 }} />
          </IconButton>
        </div>
        <AddNewEmployeeCard open={openDialog} onClose={handleClose} />
      </div>
      <EmployeesSummaryTable />
    </div>
  );
};

export default Employees;
