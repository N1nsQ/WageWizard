import { Box, CircularProgress } from "@mui/material";

interface LoadingOverlayProps {
  isLoading: boolean;
}

const LoadingOverlay = ({ isLoading }: LoadingOverlayProps) => {
  if (!isLoading) return null;

  return (
    <Box className="loading-overlay">
      <CircularProgress color="inherit" />
    </Box>
  );
};

export default LoadingOverlay;
