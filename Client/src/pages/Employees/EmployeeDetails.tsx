import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../redux/store";
import { useEffect } from "react";
import { fetchEmployeeDetails } from "../../redux/slices/EmployeeDetailsSlice";
import { useParams } from "react-router-dom";

const EmployeeDetails = () => {
  const { id } = useParams<{ id: string }>();
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

  // Logittaa tiedot, kun ne ovat saatavilla
  useEffect(() => {
    if (employee) {
      console.log("Employee details:", employee);
    }
  }, [employee]);

  console.log("Dispatching fetchEmployeeDetails with id:", id);

  return <div>Employee Details</div>;
};

export default EmployeeDetails;
