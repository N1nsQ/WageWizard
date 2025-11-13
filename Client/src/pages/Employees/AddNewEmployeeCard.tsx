import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import { useTranslation } from "react-i18next";

import AddNewEmployeeForm from "./AddNewEmployeeForm";

interface Props {
  open: boolean;
  onClose: () => void;
}

const AddNewEmployeeCard = ({ open, onClose }: Props) => {
  const { t } = useTranslation();

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{t("employees.add_new_employee")}</DialogTitle>
      <DialogContent>
        <AddNewEmployeeForm />
      </DialogContent>
      <DialogActions>
        <Button color="secondary" onClick={onClose}>
          {t("employees.cancel")}
        </Button>
        <Button variant="contained" color="primary">
          {t("employees.save")}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddNewEmployeeCard;
