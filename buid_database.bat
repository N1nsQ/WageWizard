@echo off
setlocal enabledelayedexpansion

rem === ASETUKSET ==================================================
set "SERVER=."
set "DATABASE=WageWizard"
set "DB_FOLDER=%~dp0DB"
set "SQLCMD=sqlcmd"
rem ================================================================

echo =============================================
echo SQL-tietokannan ajoskripti - %date% %time%
echo =============================================

rem --- Tarkistetaan että kansio on olemassa ---
if not exist "%DB_FOLDER%" (
    echo Virhe: kansiota "%DB_FOLDER%" ei loydy.
    pause
    exit /b 1
)

rem --- Tarkistetaan että sqlcmd löytyy ---
where %SQLCMD% >nul 2>&1
if errorlevel 1 (
    echo Virhe: sqlcmd-tyokalu ei loydy PATHista.
    echo Asenna SQLCMD tai lisää se PATH-ympäristömuuttujaan.
    pause
    exit /b 1
)

rem --- Luo lokitiedosto aikaleimalla ---
for /f "tokens=1-3 delims=/. " %%a in ("%date%") do (
    for /f "tokens=1-2 delims=: " %%x in ("%time%") do (
        set "TS=%%a-%%b-%%c_%%x-%%y"
    )
)
set "LOGFILE=%~dp0run_sql_%TS%.log"
echo Kirjoitetaan lokiin: %LOGFILE%
echo. > "%LOGFILE%"

rem --- Tulostetaan info mikä SQL-instanssi ja käyttäjä ---
"%SQLCMD%" -S "%SERVER%" -E -Q "SELECT @@SERVERNAME AS ServerInstance, SYSTEM_USER AS UserName" >> "%LOGFILE%" 2>&1

rem --- Suoritetaan kaikki SQL-tiedostot aakkosjärjestyksessä ---
pushd "%DB_FOLDER%"
for %%F in (*.sql) do (
    echo.
    echo Suoritetaan: %%F
    echo [%%F] >> "%LOGFILE%"

    rem Jos tiedoston nimessä on CREATE_DATABASE, ajetaan master-kannassa
    echo %%F | find /I "CREATE_DATABASE" >nul
    if not errorlevel 1 (
        "%SQLCMD%" -S "%SERVER%" -d "master" -E -b -i "%%F" >> "%LOGFILE%" 2>&1
    ) else (
        "%SQLCMD%" -S "%SERVER%" -d "%DATABASE%" -E -b -i "%%F" >> "%LOGFILE%" 2>&1
    )

    if errorlevel 1 (
        echo VIRHE tiedostossa %%F. Katso loki: "%LOGFILE%"
        popd
        pause
        exit /b 1
    )
)
popd

rem --- Tarkistetaan, että tietokanta on luotu ---
"%SQLCMD%" -S "%SERVER%" -d "master" -E -Q "IF DB_ID('%DATABASE%') IS NULL RAISERROR('Tietokantaa ei löytynyt luomisen jälkeen.',16,1)" >> "%LOGFILE%" 2>&1
if errorlevel 1 (
    echo Virhe: tietokantaa ei luotu onnistuneesti. Katso loki: "%LOGFILE%"
    pause
    exit /b 1
)

echo.
echo =============================================
echo Kaikki SQL-tiedostot suoritettu onnistuneesti.
echo Lokitiedosto: "%LOGFILE%"
echo =============================================
pause
exit /b 0
