import { Box, Card, CardContent, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export function Home() {
  return (
    <>
      <div>
        <Typography variant="h1">Etusivu</Typography>
      </div>
      <div>
        <Card sx={{ width: 400, borderRadius: 3, boxShadow: 4 }}>
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              Sopimukset
            </Typography>

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
            ></Box>
          </CardContent>
        </Card>
      </div>
      <div>
        <Card sx={{ width: 400, borderRadius: 3, boxShadow: 4 }}>
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              Palkkalaskelmat
            </Typography>

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
            ></Box>
          </CardContent>
        </Card>
      </div>

      <div>
        <Card
          component={Link}
          to="/employees"
          sx={{
            display: "block",
            width: 400,
            borderRadius: 3,
            boxShadow: 4,
            textDecoration: "none",
            color: "inherit",
            transition: "transform 0.2s, box-shadow 0.2s",
            "&:hover": {
              transform: "scale(1.02)",
              boxShadow: 6,
            },
          }}
        >
          <CardContent>
            <Typography variant="h5" align="center" gutterBottom>
              Työntekijät
            </Typography>

            <Box
              component="form"
              display="flex"
              flexDirection="column"
              gap={2}
            ></Box>
          </CardContent>
        </Card>
      </div>
    </>
  );
}
