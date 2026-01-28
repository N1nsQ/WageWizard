-- 1. Lisää UserId-sarake (nullable aluksi)
ALTER TABLE Employees 
ADD UserId UNIQUEIDENTIFIER NULL;

-- 2. Päivitä UserId-arvot yhdistämällä sähköpostilla
UPDATE e
SET e.UserId = u.Id
FROM Employees e
INNER JOIN Users u ON u.Email = e.Email
WHERE u.Role = 'Employee';  -- tai mitä logiikkaa käytätkään

-- 3. Tarkista että kaikki työntekijät saivat UserId:n
SELECT * FROM Employees WHERE UserId IS NULL;

-- 4. Jos joillakin puuttuu UserId, käsittele ne erikseen
-- Esim. luo heille uudet käyttäjät tai aseta oletusarvo

-- 5. Tee UserId-sarakkeesta pakollinen (NOT NULL)
ALTER TABLE Employees
ALTER COLUMN UserId UNIQUEIDENTIFIER NOT NULL;

-- 6. Lisää Foreign Key -rajoite
ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Users
FOREIGN KEY (UserId) REFERENCES Users(Id);