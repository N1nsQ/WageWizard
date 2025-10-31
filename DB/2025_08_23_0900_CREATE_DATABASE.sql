IF DB_ID('WageWizard') IS NULL
BEGIN
    PRINT 'Luodaan tietokanta WageWizard...';
    CREATE DATABASE WageWizard;
END
GO

-- Vaihdetaan juuri luotuun tietokantaan
USE WageWizard;
GO
