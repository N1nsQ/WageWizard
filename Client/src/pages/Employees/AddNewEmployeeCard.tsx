import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import { useTranslation } from "react-i18next";

import AddNewEmployeeForm from "./AddNewEmployeeForm";
import { useRef } from "react";
import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store";
import { Alert } from "@mui/material";

interface Props {
  open: boolean;
  onClose: () => void;
}

const AddNewEmployeeCard = ({ open, onClose }: Props) => {
  const { t } = useTranslation();
  const handleSubmitRef = useRef<() => void>();
  const error = useSelector((state: RootState) => state.employees.error);

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{t("employees.add_new_employee")}</DialogTitle>
      <DialogContent>
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {t(error)}
          </Alert>
        )}
        <AddNewEmployeeForm
          onClose={onClose}
          onSubmitRef={(handleSubmit) =>
            (handleSubmitRef.current = handleSubmit)
          }
        />
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {t(error)}
          </Alert>
        )}
      </DialogContent>
      <DialogActions>
        <Button color="secondary" onClick={onClose}>
          {t("employees.cancel")}
        </Button>
        <Button
          variant="contained"
          color="primary"
          onClick={() => handleSubmitRef.current?.()}
        >
          {t("employees.save")}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddNewEmployeeCard;
