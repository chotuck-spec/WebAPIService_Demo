CREATE TABLE Person (
    PersonID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Age INT NULL,
    Height DECIMAL(5,2) NULL,  -- Example: 175.50 cm
    Weight DECIMAL(5,2) NULL,  -- Example: 70.25 kg
    Email VARCHAR(100) NULL
);

INSERT INTO Person (FirstName, LastName, Age, Height, Weight, Email) VALUES
('John', 'Smith', 28, 180.50, 75.20, 'john.smith@email.com'),
('Emily', 'Johnson', 32, 165.30, 60.10, 'emily.j@email.com'),
('Michael', 'Brown', 40, 178.00, 82.50, 'michael.brown@email.com'),
('Sarah', 'Lee', 25, 160.20, 55.00, 'sarah.lee@email.com'),
('David', 'Wilson', 36, 172.40, 68.90, 'david.wilson@email.com'),
('Sophia', 'Davis', 29, 168.70, 59.40, 'sophia.davis@email.com'),
('Daniel', 'Miller', 42, 181.60, 90.30, 'daniel.miller@email.com'),
('Olivia', 'Garcia', 31, 158.90, 52.70, 'olivia.garcia@email.com'),
('James', 'Martinez', 27, 175.80, 72.60, 'james.martinez@email.com'),
('Ava', 'Taylor', 33, 162.50, 58.20, 'ava.taylor@email.com');