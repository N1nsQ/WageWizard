import { Button } from "@mui/material";
import EmployeeDetailsPage from "./EmployeeDetails";

const Profile = () => {
  const user = "User";

  return (
    <div>
      <EmployeeDetailsPage />
      <p>Hello {user}</p>
      <Button />
    </div>
  );
};

export default Profile;
