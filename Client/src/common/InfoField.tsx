import { Box, Typography } from "@mui/material";

interface InfoFieldProps {
  label: string;
  value: string | number | JSX.Element | undefined;
}

const InfoField = ({ label, value }: InfoFieldProps) => (
  <Box mb={2}>
    <Typography variant="subtitle2" color="textSecondary">
      {label}
    </Typography>
    <Typography variant="body1">{value || "-"}</Typography>
  </Box>
);

export default InfoField;
