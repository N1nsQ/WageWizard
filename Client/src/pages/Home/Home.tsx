import { Box, Card, CardContent, Typography } from "@mui/material";

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
        <Card sx={{ width: 400, borderRadius: 3, boxShadow: 4 }}>
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
