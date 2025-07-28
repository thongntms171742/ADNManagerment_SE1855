

CREATE DATABASE ADNManagerment;
GO
USE ADNManagerment;
GO

-- Create User table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(255),
    DateOfBirth DATE,
    Role INT NOT NULL, -- 0: Guest, 1: Customer, 2: Staff, 3: Manager, 4: Admin
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create Service table
CREATE TABLE Services (
    ServiceId INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18, 2) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastUpdatedDate DATETIME
);

-- Create Booking table
CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    ServiceId INT NOT NULL,
    BookingDate DATE NOT NULL,
    Status INT NOT NULL, -- 0: Registered, 1: KitSent, 2: SampleCollected, 3: SampleReceived, 4: Testing, 5: Completed, 6: Cancelled
    CollectionMethod INT NOT NULL, -- 0: SelfCollection, 1: HomeCollection, 2: OnSiteCollection
    ShippingAddress NVARCHAR(255),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Users(UserId),
    FOREIGN KEY (ServiceId) REFERENCES Services(ServiceId)
);

-- Create Sample table
CREATE TABLE Samples (
    SampleId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL UNIQUE,
    SampleType NVARCHAR(50) NOT NULL,
    CollectionDate DATE NOT NULL,
    ReceivedDate DATE NOT NULL,
    StorageLocation NVARCHAR(100),
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
);

-- Create Result table
CREATE TABLE Results (
    ResultId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL UNIQUE,
    ResultDetails NVARCHAR(MAX) NOT NULL,
    ResultDate DATE NOT NULL,
    StaffId INT NOT NULL,
    TestStatus NVARCHAR(50) NOT NULL DEFAULT 'Completed', -- Completed, In Progress, Failed
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId),
    FOREIGN KEY (StaffId) REFERENCES Users(UserId)
);

-- Create Feedback table
CREATE TABLE Feedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    BookingId INT NOT NULL,
    Rating INT NOT NULL, -- 1-5 stars
    Comment NVARCHAR(MAX),
    FeedbackDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Users(UserId),
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
);


INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Admin ADNBL', 'admin@adnbl.com', 'admin@ADN', '0123456701', 'Hanoi, Vietnam', '1980-01-01', 4);

INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Manager ADNBL', 'manager@adnbl.com', 'manager@ADN', '0123456702', 'Da Nang, Vietnam', '1985-05-15', 3);

INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Staff ADNBL', 'staff@adnbl.com', 'staff@ADN', '0123456703', 'Ho Chi Minh, Vietnam', '1990-09-20', 2);

-- Insert customers with full info
INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Nguyen Van A', 'nguyenvana@gmail.com', 'customerA@ADN', '0901000001', 'Hanoi, Vietnam', '1992-02-10', 1);
INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Tran Thi B', 'tranthib@gmail.com', 'customerB@ADN', '0901000002', 'Da Nang, Vietnam', '1993-03-15', 1);
INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Le Van C', 'levanc@gmail.com', 'customerC@ADN', '0901000003', 'Hai Phong, Vietnam', '1994-04-20', 1);
INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Pham Thi D', 'phamthid@gmail.com', 'customerD@ADN', '0901000004', 'Can Tho, Vietnam', '1995-05-25', 1);
INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Hoang Van E', 'hoangvane@gmail.com', 'customerE@ADN', '0901000005', 'Vung Tau, Vietnam', '1996-06-30', 1);

INSERT INTO Services (ServiceName, Description, Price) VALUES
('Paternity DNA Test', 'DNA test to determine biological relationship between father and child', 2500000),
('Maternity DNA Test', 'DNA test to determine biological relationship between mother and child', 2500000),
('Sibling DNA Test', 'DNA test to determine if two individuals are full or half siblings', 3000000),
('Grandparent DNA Test', 'DNA test to determine biological relationship with grandparents', 3500000),
('Twin Zygosity Test', 'Test to determine if twins are identical or fraternal', 2000000),
('Y-Chromosome DNA Test', 'Test for paternal lineage using Y-chromosome analysis', 3200000),
('Mitochondrial DNA Test', 'Test for maternal lineage using mitochondrial DNA', 3200000),
('Prenatal Paternity Test', 'Non-invasive prenatal paternity test', 8000000),
('Ancestry DNA Test', 'Test to determine ancestry and ethnic origins', 4000000),
('Genetic Health Risk Test', 'Test to assess genetic risk for certain health conditions', 5000000);

-- Insert Bookings: 2-3 bookings per month for 2025, for users 1 (admin), 2 (manager), 3 (staff)
-- January 2025
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (1, 1, '2025-01-10', 0, 0, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (2, 2, '2025-01-15', 1, 1, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (3, 3, '2025-01-20', 2, 2, 'Ho Chi Minh, Vietnam');

-- February 2025
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (1, 4, '2025-02-05', 0, 1, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (2, 5, '2025-02-18', 1, 0, 'Da Nang, Vietnam');

-- March 2025
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (3, 6, '2025-03-03', 2, 2, 'Ho Chi Minh, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (1, 7, '2025-03-15', 3, 1, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (2, 8, '2025-03-28', 4, 0, 'Da Nang, Vietnam');

-- April 2025
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (3, 9, '2025-04-10', 5, 2, 'Ho Chi Minh, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (1, 10, '2025-04-22', 6, 1, 'Hanoi, Vietnam');

INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (2, 1, '2025-05-05', 0, 0, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (3, 2, '2025-05-18', 1, 1, 'Ho Chi Minh, Vietnam');

-- Diverse bookings for customers (users 4-8)
-- Nguyen Van A (user 4)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 1, '2025-01-12', 0, 0, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 5, '2025-03-22', 1, 1, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 9, '2025-06-10', 2, 2, 'Hanoi, Vietnam');

-- Tran Thi B (user 5)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 2, '2025-02-14', 0, 1, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 6, '2025-04-18', 1, 0, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 10, '2025-07-05', 2, 2, 'Da Nang, Vietnam');

-- Le Van C (user 6)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 3, '2025-01-25', 0, 2, 'Hai Phong, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 7, '2025-05-12', 1, 1, 'Hai Phong, Vietnam');

-- Pham Thi D (user 7)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 4, '2025-03-08', 0, 0, 'Can Tho, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 8, '2025-06-20', 1, 2, 'Can Tho, Vietnam');

-- Hoang Van E (user 8)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 1, '2025-02-28', 0, 1, 'Vung Tau, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 4, '2025-04-30', 1, 0, 'Vung Tau, Vietnam');
