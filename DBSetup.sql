
IF DB_ID('TaxCalculator') IS NULL
    CREATE DATABASE TaxCalculator;
GO

USE TaxCalculator;
GO

CREATE TABLE Cities (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE VehicleTypes (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE CityVehicleTypeRules (
    Id INT IDENTITY PRIMARY KEY,
    CityId INT NOT NULL,
    VehicleTypeId INT NOT NULL,
    FOREIGN KEY (CityId) REFERENCES Cities(Id),
    FOREIGN KEY (VehicleTypeId) REFERENCES VehicleTypes(Id)
);
GO

CREATE TABLE CityTaxRules (
    Id INT IDENTITY PRIMARY KEY,
    CityId INT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    Amount INT NOT NULL,
    FOREIGN KEY (CityId) REFERENCES Cities(Id)
);
GO

CREATE TABLE CityHolidays (
    Id INT IDENTITY PRIMARY KEY,
    CityId INT NOT NULL,
    HolidayDate DATETIME NOT NULL,
    FOREIGN KEY (CityId) REFERENCES Cities(Id)
);
GO

CREATE TABLE Vehicles (
    Id INT IDENTITY PRIMARY KEY,
    LicensePlate NVARCHAR(20) NOT NULL,
    VehicleTypeId INT NOT NULL,
    FOREIGN KEY (VehicleTypeId) REFERENCES VehicleTypes(Id)
);
GO

CREATE TABLE VehiclePassings (
    Id INT IDENTITY PRIMARY KEY,
    VehicleId INT NOT NULL,
    CityId INT NOT NULL,
    PassingDateTime DATETIME NOT NULL,
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id) ON DELETE CASCADE,
    FOREIGN KEY (CityId) REFERENCES Cities(Id) ON DELETE CASCADE
);
GO

IF NOT EXISTS( SELECT * FROM dbo.Cities WHERE Name = 'Gothenburg')
BEGIN 
INSERT INTO Cities (Name) VALUES ('Gothenburg');
END 
GO




IF NOT EXISTS (SELECT 1 FROM VehicleTypes)
BEGIN 
INSERT INTO VehicleTypes (Name)
VALUES ('Car'), ('Motorcycle'), ('Bus'), ('Diplomat'), ('Emergency'), ('Military'), ('Foreign');
END 
GO

IF NOT EXISTS (SELECT 1 FROM CityVehicleTypeRules)
BEGIN 
DECLARE @CityId INT = (SELECT Id FROM Cities WHERE Name='Gothenburg');
INSERT INTO CityVehicleTypeRules (CityId, VehicleTypeId)
SELECT @CityId, Id
FROM VehicleTypes where Name <> 'Car';
END 
GO

IF NOT EXISTS (SELECT 1 FROM CityTaxRules)
BEGIN 
DECLARE @CityId INT = (SELECT Id FROM Cities WHERE Name='Gothenburg');
INSERT INTO CityTaxRules (CityId, StartTime, EndTime, Amount)
VALUES
(@CityId, '06:00', '06:29', 8),
(@CityId, '06:30', '06:59', 13),
(@CityId, '07:00', '07:59', 18),
(@CityId, '08:00', '08:29', 13),
(@CityId, '08:30', '14:59', 8),
(@CityId, '15:00', '15:29', 13),
(@CityId, '15:30', '16:59', 18),
(@CityId, '17:00', '17:59', 13),
(@CityId, '18:00', '18:29', 8);
END 
GO


IF NOT EXISTS (SELECT 1 FROM CityHolidays)
BEGIN 
DECLARE @CityId INT = (SELECT Id FROM Cities WHERE Name='Gothenburg');
DECLARE @Year INT = 2013;
INSERT INTO CityHolidays (CityId, HolidayDate)
VALUES
    (@CityId, DATEFROMPARTS(@Year, 1, 1)), 
    (@CityId, DATEFROMPARTS(@Year, 3, 28)),  
    (@CityId, DATEFROMPARTS(@Year, 3, 29)), 
    (@CityId, DATEFROMPARTS(@Year, 4, 1)), 
    (@CityId, DATEFROMPARTS(@Year, 4, 30)), 
    (@CityId, DATEFROMPARTS(@Year, 5, 1)), 
    (@CityId, DATEFROMPARTS(@Year, 5, 8)), 
    (@CityId, DATEFROMPARTS(@Year, 5, 9)), 
    (@CityId, DATEFROMPARTS(@Year, 6, 5)),  
    (@CityId, DATEFROMPARTS(@Year, 6, 6)), 
    (@CityId, DATEFROMPARTS(@Year, 6, 21)), 
    (@CityId, DATEFROMPARTS(@Year, 11, 1)), 
    (@CityId, DATEFROMPARTS(@Year, 12, 24)),
    (@CityId, DATEFROMPARTS(@Year, 12, 25)), 
    (@CityId, DATEFROMPARTS(@Year, 12, 26)), 
    (@CityId, DATEFROMPARTS(@Year, 12, 31));
END 
GO
