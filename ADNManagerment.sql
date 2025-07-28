

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

-- Insert Admin, Manager, Staff (Role 2,3,4 - không có booking)
INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Admin ADNBL', 'admin@adnbl.com', 'admin@ADN', '0123456701', 'Hanoi, Vietnam', '1980-01-01', 4);

INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Manager ADNBL', 'manager@adnbl.com', 'manager@ADN', '0123456702', 'Da Nang, Vietnam', '1985-05-15', 3);

INSERT INTO Users (FullName, Email, Password, PhoneNumber, Address, DateOfBirth, Role)
VALUES ('Staff ADNBL', 'staff@adnbl.com', 'staff@ADN', '0123456703', 'Ho Chi Minh, Vietnam', '1990-09-20', 2);

-- Insert customers with full info (Role 1 - có booking)
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

-- Insert Bookings chỉ cho customers (users 4-8, role 1)
-- Nguyen Van A (user 4)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 1, '2025-01-12', 5, 0, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 5, '2025-03-22', 5, 1, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 9, '2025-06-10', 4, 2, 'Hanoi, Vietnam');

-- Tran Thi B (user 5)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 2, '2025-02-14', 5, 1, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 6, '2025-04-18', 5, 0, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 10, '2025-07-05', 3, 2, 'Da Nang, Vietnam');

-- Le Van C (user 6)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 3, '2025-01-25', 5, 2, 'Hai Phong, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 7, '2025-05-12', 5, 1, 'Hai Phong, Vietnam');

-- Pham Thi D (user 7)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 4, '2025-03-08', 5, 0, 'Can Tho, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 8, '2025-06-20', 4, 2, 'Can Tho, Vietnam');

-- Hoang Van E (user 8)
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 1, '2025-02-28', 5, 1, 'Vung Tau, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 4, '2025-04-30', 5, 0, 'Vung Tau, Vietnam');

-- Thêm booking cho các tháng khác để có dữ liệu đầy đủ
-- Tháng 1: 3 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 2, '2025-01-05', 5, 0, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 3, '2025-01-18', 5, 1, 'Da Nang, Vietnam');

-- Tháng 2: 4 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 4, '2025-02-03', 5, 2, 'Hai Phong, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 5, '2025-02-15', 5, 0, 'Can Tho, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 6, '2025-02-22', 5, 1, 'Hanoi, Vietnam');

-- Tháng 3: 5 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 7, '2025-03-10', 5, 2, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 8, '2025-03-18', 5, 0, 'Hai Phong, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 9, '2025-03-25', 5, 1, 'Can Tho, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 10, '2025-03-30', 5, 2, 'Vung Tau, Vietnam');

-- Tháng 4: 6 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 1, '2025-04-05', 5, 0, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 2, '2025-04-12', 5, 1, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 3, '2025-04-20', 5, 2, 'Hai Phong, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 4, '2025-04-28', 5, 0, 'Can Tho, Vietnam');

-- Tháng 5: 7 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 5, '2025-05-03', 5, 1, 'Vung Tau, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 6, '2025-05-10', 5, 2, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 7, '2025-05-18', 5, 0, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 8, '2025-05-25', 5, 1, 'Hai Phong, Vietnam');

-- Tháng 6: 8 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 9, '2025-06-02', 5, 2, 'Can Tho, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 10, '2025-06-08', 5, 0, 'Vung Tau, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 1, '2025-06-15', 5, 1, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 2, '2025-06-22', 5, 2, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 3, '2025-06-30', 5, 0, 'Hai Phong, Vietnam');

-- Tháng 7: 9 bookings
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (7, 4, '2025-07-05', 5, 1, 'Can Tho, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (8, 5, '2025-07-12', 5, 2, 'Vung Tau, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (4, 6, '2025-07-18', 5, 0, 'Hanoi, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (5, 7, '2025-07-25', 5, 1, 'Da Nang, Vietnam');
INSERT INTO Bookings (CustomerId, ServiceId, BookingDate, Status, CollectionMethod, ShippingAddress)
VALUES (6, 8, '2025-07-30', 5, 2, 'Hai Phong, Vietnam');

-- Insert Samples cho các booking đã hoàn thành
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (1, 'Buccal Swab', '2025-01-15', '2025-01-18', 'Freezer A-01');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (2, 'Blood Sample', '2025-03-25', '2025-03-28', 'Freezer B-02');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (3, 'Buccal Swab', '2025-06-12', '2025-06-15', 'Freezer A-03');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (4, 'Blood Sample', '2025-02-16', '2025-02-19', 'Freezer B-04');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (5, 'Buccal Swab', '2025-04-20', '2025-04-23', 'Freezer A-05');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (6, 'Buccal Swab', '2025-01-27', '2025-01-30', 'Freezer B-06');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (7, 'Blood Sample', '2025-05-14', '2025-05-17', 'Freezer A-07');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (8, 'Buccal Swab', '2025-03-10', '2025-03-13', 'Freezer B-08');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (9, 'Blood Sample', '2025-06-22', '2025-06-25', 'Freezer A-09');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (10, 'Buccal Swab', '2025-03-02', '2025-03-05', 'Freezer B-10');
INSERT INTO Samples (BookingId, SampleType, CollectionDate, ReceivedDate, StorageLocation)
VALUES (11, 'Blood Sample', '2025-05-02', '2025-05-05', 'Freezer A-11');

-- Insert Results cho các booking đã hoàn thành (Status = 5)
-- Staff (user 3) sẽ xử lý các kết quả
INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (1, 'Paternity DNA Test Result: POSITIVE - 99.9% probability of biological relationship. DNA markers analyzed: 16 loci. Sample quality: Excellent. Conclusion: The tested individual is the biological father of the child.', '2025-01-25', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (2, 'Twin Zygosity Test Result: IDENTICAL TWINS - 99.99% probability of monozygotic twins. Genetic markers analyzed: 20 loci. Sample quality: Good. Conclusion: The twins are identical (monozygotic).', '2025-04-05', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (4, 'Maternity DNA Test Result: POSITIVE - 99.8% probability of biological relationship. DNA markers analyzed: 16 loci. Sample quality: Excellent. Conclusion: The tested individual is the biological mother of the child.', '2025-02-25', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (5, 'Y-Chromosome DNA Test Result: PATERNAL LINEAGE CONFIRMED - Y-chromosome markers match paternal line. Markers analyzed: 17 Y-STR loci. Sample quality: Good. Conclusion: Paternal lineage is confirmed through Y-chromosome analysis.', '2025-05-05', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (6, 'Sibling DNA Test Result: FULL SIBLINGS - 95.2% probability of full sibling relationship. DNA markers analyzed: 18 loci. Sample quality: Excellent. Conclusion: The tested individuals are full siblings.', '2025-02-05', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (7, 'Mitochondrial DNA Test Result: MATERNAL LINEAGE CONFIRMED - Mitochondrial DNA markers match maternal line. Markers analyzed: 15 mtDNA loci. Sample quality: Good. Conclusion: Maternal lineage is confirmed through mitochondrial DNA analysis.', '2025-05-25', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (8, 'Grandparent DNA Test Result: POSITIVE - 98.5% probability of grandparent relationship. DNA markers analyzed: 16 loci. Sample quality: Excellent. Conclusion: The tested individual is the biological grandparent.', '2025-03-20', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (10, 'Paternity DNA Test Result: POSITIVE - 99.7% probability of biological relationship. DNA markers analyzed: 16 loci. Sample quality: Good. Conclusion: The tested individual is the biological father of the child.', '2025-03-10', 3, 'Completed');

INSERT INTO Results (BookingId, ResultDetails, ResultDate, StaffId, TestStatus)
VALUES (11, 'Grandparent DNA Test Result: POSITIVE - 97.8% probability of grandparent relationship. DNA markers analyzed: 16 loci. Sample quality: Good. Conclusion: The tested individual is the biological grandparent.', '2025-05-10', 3, 'Completed');

-- Insert Feedback cho các booking đã hoàn thành
INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (4, 1, 5, 'Excellent service! The staff was very professional and the results were delivered on time. Highly recommend!', '2025-01-26');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (4, 2, 4, 'Good service overall. Results were accurate and the process was smooth.', '2025-04-06');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (5, 4, 5, 'Outstanding service! Very professional team and accurate results. Thank you!', '2025-02-26');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (5, 5, 4, 'Good experience. The testing process was clear and results were reliable.', '2025-05-06');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (6, 6, 5, 'Excellent quality service. Staff was knowledgeable and helpful throughout the process.', '2025-02-06');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (6, 7, 4, 'Satisfied with the service. Results were delivered as promised.', '2025-05-26');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (7, 8, 5, 'Very professional service. The team was courteous and results were accurate.', '2025-03-21');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (8, 10, 4, 'Good service. The process was straightforward and results were reliable.', '2025-03-11');

INSERT INTO Feedbacks (CustomerId, BookingId, Rating, Comment, FeedbackDate)
VALUES (8, 11, 5, 'Excellent service! Very satisfied with the results and professional staff.', '2025-05-11');
