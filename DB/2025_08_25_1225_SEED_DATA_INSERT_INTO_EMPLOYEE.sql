INSERT INTO Employees (Id, FirstName, LastName, JobTitle, Email, HomeAddress, PostalCode, City, BankAccountNumber, TaxRate, GrossSalary, StartDate, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'Minni', 'Hiiri', 'sihteeri', 'minni.hiiri@dreamwork.com', 'Kiisselikuja 3', '12345', 'Ankkalinna', 'FI5847269664000292', 11.0, 3200.00, '2020-05-01', GETDATE(), NULL),
(NEWID(), 'Ariel', 'Mermaid', 'vokalisti', 'ariel.mermaid@dreamwork.com', 'Rantakatu 136', '12123', 'Rannikko', 'FI3147070797000849', 4.5, 1250.50, '2023-03-02', GETDATE(), NULL),
(NEWID(), 'Roosa', 'Ruusunen', 'kosmetologi', 'roosa.ruusunen@dreamwork.com', 'Piikkikuja 4 c 14', '30300', 'Korkealinna', 'FI7240517266000376', 9.0, 2000.00, '2022-12-01', GETDATE(), NULL);