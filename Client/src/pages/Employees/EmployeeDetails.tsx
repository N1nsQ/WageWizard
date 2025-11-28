import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useTranslation } from "react-i18next";

import type { AppDispatch, RootState } from "../../redux/store";
import { fetchEmployeeById } from "../../redux/slices/EmployeesSlice";

import InfoField from "../../common/InfoField";
import Img300px from "../../common/Img300px";

import { formatCurrency } from "../../utils/formatCurrency";
import { formatIban } from "../../utils/formatIban";
import { formatDate } from "../../utils/formatDate";

import {
  Box,
  Card,
  CardContent,
  Divider,
  Stack,
  Typography,
} from "@mui/material";
import "../../App.css";

const EmployeeDetailsPage = () => {
  const { id } = useParams<{ id: string }>();
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();

  const employee = useSelector(
    (state: RootState) => state.employees.selectedEmployee
  );

  useEffect(() => {
    if (id) {
      dispatch(fetchEmployeeById(id));
    }
  }, [dispatch, id]);

  return (
    <div className="center-content">
      <Card>
        <CardContent>
          <Stack spacing={2} textAlign={"left"} margin={3}>
            <Img300px
              imageUrl={employee?.imageUrl ?? null}
              alt={t("employees.employeeImg")}
            />

            <InfoField
              label={t("employees.name")}
              value={`${employee?.firstName} ${employee?.lastName}`}
            />
            <Divider />
            {/* <InfoField label={t("employees.age")} value={`${employee?.age}`} />
            <Divider /> */}
            <InfoField
              label={t("employees.jobtitle")}
              value={employee?.jobTitle}
            />
            <Divider />
            <InfoField label={t("employees.email")} value={employee?.email} />
            <Divider />
            <Box mb={2}>
              <Typography variant="subtitle2" color="textSecondary">
                {t("employees.homeAddress")}
              </Typography>
              <Typography variant="body1">
                {employee?.homeAddress || "-"}
              </Typography>
              <Typography variant="body1">
                {employee?.postalCode} {employee?.city}
              </Typography>
            </Box>
            <Divider />
            <InfoField
              label={t("employees.bankAccountNumber")}
              value={formatIban(employee?.bankAccountNumber)}
            />
            <Divider />
            <InfoField
              label={t("employees.taxPercentage")}
              value={`${employee?.taxRate}%`}
            />
            <Divider />
            <InfoField
              label={t("employees.salaryAmount")}
              value={formatCurrency(employee?.grossSalary)}
            />
            <Divider />
            <InfoField
              label={t("employees.startDate")}
              value={formatDate(employee?.startDate)}
            />
          </Stack>
        </CardContent>
      </Card>
    </div>
  );
};

export default EmployeeDetailsPage;
