import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/store";
import { useEffect } from "react";
import { fetchEmployeeDetails } from "../../redux/slices/EmployeeDetailsSlice";
import { useParams } from "react-router-dom";
import { Card, CardContent, CardMedia, Stack, Typography } from "@mui/material";
import Grid from "@mui/material/Grid";
import Img300px from "../../common/Img300px";
import { useTranslation } from "react-i18next";

const EmployeeDetails = () => {
  const { id } = useParams<{ id: string }>();
  const { t } = useTranslation();
  const dispatch = useDispatch<AppDispatch>();

  const {
    data: employee,
    isLoading,
    error,
  } = useSelector((state: RootState) => state.employeeDetails);

  useEffect(() => {
    if (id) {
      dispatch(fetchEmployeeDetails(id));
    }
  }, [dispatch, id]);

  return (
    <div>
      <Card>
        <CardContent>
          <div>
            <Grid>
              <Img300px
                imageUrl={employee?.imageUrl ?? null}
                alt={t("employees.employeeImg")}
              />
            </Grid>
          </div>

          <Grid container spacing={2}>
            <Typography variant="h4">
              {employee?.firstName} {employee?.lastName}
            </Typography>
            <Typography variant="body1">{employee?.jobTitle}</Typography>
          </Grid>

          <Grid container spacing={2}>
            <Grid size={8}>
              <Typography variant="subtitle2">Email</Typography>
              <Typography>{employee?.email}</Typography>
            </Grid>

            <Grid size={8}>
              <Typography variant="subtitle2">Address</Typography>
              <Typography>
                {employee?.homeAddress + ", "} {employee?.postalCode}{" "}
                {employee?.city}
              </Typography>
            </Grid>

            <Grid size={8}>
              <Typography variant="subtitle2">Bank Account</Typography>
              <Typography>{employee?.bankAccountNumber}</Typography>
            </Grid>

            <Grid size={8}>
              <Typography variant="subtitle2">tax</Typography>
              <Typography>{employee?.taxPercentage}</Typography>
            </Grid>

            <Grid size={8}>
              <Typography variant="subtitle2">Start Date</Typography>
              <Typography>
                {employee?.startDate
                  ? new Date(employee.startDate).toLocaleDateString()
                  : ""}
              </Typography>
            </Grid>

            <Grid size={8}>
              <Typography variant="subtitle2">Salary</Typography>
              <Typography>{employee?.salaryAmount}</Typography>
            </Grid>
          </Grid>
        </CardContent>
      </Card>
    </div>
  );
};

export default EmployeeDetails;
