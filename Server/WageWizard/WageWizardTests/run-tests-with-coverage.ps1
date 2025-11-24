dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"TestResults\**\coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html
start coverage-report/index.html