﻿
CREATE TABLE FieldType(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	normalDayPrice FLOAT NOT NULL,
	specialDayPrice FLOAT NOT NULL
)
CREATE TABLE FieldName(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	status NVARCHAR(100) NOT NULL,
	idFieldType INT NOT NULL,
	FOREIGN KEY (idFieldType) REFERENCES dbo.fieldType(id)
)

CREATE TABLE Customer(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	phone NVARCHAR(100) NOT NULL
)

CREATE TABLE CustomerBooking(
	id INT IDENTITY PRIMARY KEY,
	idCustomer INT NOT NULL,
	idFieldName INT NOT NULL,
	startTime TIME NOT NULL,
	endTime TIME DEFAULT GETDATE(),
	priceBooking FLOAT NOT NULL,
	status NVARCHAR(100) NOT NULL,
	FOREIGN KEY (idCustomer) REFERENCES dbo.Customer(id),
	FOREIGN KEY (idFieldName) REFERENCES dbo.FieldName(id),
)


CREATE TABLE Bill(
	id INT IDENTITY PRIMARY KEY,
	idCustomerBooking INT NOT NULL,
	datePayment DATETIME NOT NULL DEFAULT GETDATE(),
	totalPrice FLOAT NOT NULL,
	FOREIGN KEY (idCustomerBooking) REFERENCES dbo.CustomerBooking(id),
)

CREATE TABLE Account(
	username NVARCHAR(100),
	password NVARCHAR(100),
)

INSERT INTO dbo.Account(username,password)	
VALUES ('nguyentien','240103')
INSERT INTO dbo.Account(username,password)	
VALUES ('phamnhi','150402')

SElECT * FROM dbo.Account --Transact(T)-SQL 

GO --GO signals the end of a batch
Create procedure GetAccountByUserName 
@userName nvarchar(100)
as
begin
	SElECT * FROM dbo.Account where username = @userName
end
GO
EXEC dbo.GetAccountByUserName @userName = 'nguyentien';
GO


Select * from dbo.FieldType


-- insert value for table fieldName
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('A','empty',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('B','empty',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('C','empty',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('D','empty',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('A','empty',2)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('B','empty',2)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('C','busy',2)



-- set kiểu dữ liệu
alter table dbo.FieldType
add TypeName NVARCHAR(100)

-- del table
alter table dbo.FieldType
drop column TypeName;



Select * from dbo.FieldType
GO
-- get field
CREATE PROC GetFieldList
as
begin
Select * from dbo.FieldName
end
GO
	
EXEC dbo.GetFieldList		

GO
CREATE PROC GetFieldType
as
begin
Select * from dbo.FieldType
end
GO
EXEC dbo.GetFieldType

GO

-----29/4----
EXEC dbo.GetFieldType

EXEC dbo.GetFieldList 

Select * FROM dbo.Bill
Select * FROM dbo.CustomerBooking where idFieldName =19
Select * FROM dbo.Customer 
Select * FROM dbo.FieldName 
Select * FROM dbo.FieldType 

-- lấy ra vị trí cuối cùng
SELECT * FROM dbo.Customer  WHERE id = (SELECT MAX(id) FROM dbo.Customer );
SELECT TOP 1 * FROM dbo.Customer  ORDER BY id DESC
SELECT TOP 1 * FROM dbo.FieldName   ORDER BY id DESC
SELECT * FROM dbo.FieldName  WHERE idFieldType=1 and id = (SELECT MAX(id) FROM dbo.FieldName ) 

SELECT * FROM dbo.FieldName
WHERE idFieldType = 1 
AND id = (
    SELECT MAX(id) FROM dbo.FieldName 
    WHERE idFieldType = 1
)

SELECT * FROM dbo.CustomerBooking
WHERE idFieldName = 15 
AND id = (
    SELECT MAX(id) FROM dbo.CustomerBooking 
    WHERE idFieldName = 15
)
-- insert vao dbo.Customer
INSERT INTO dbo.Customer(name,phone)	
VALUES ('NguyenQuocTien','123')
INSERT INTO dbo.Customer(name,phone)	
VALUES ('LeHaiKhoa','456')
INSERT INTO dbo.Customer(name,phone)	
VALUES ('NguyenNhatQuan','789')

-- insert vao dbo.CustomerBooking

INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,priceBooking,status)	
VALUES (1,1,GETDATE(),50000,'dadat')
INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,priceBooking,status)	
VALUES (2,2,GETDATE(),50000,'dadat')
INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,priceBooking,status)	
VALUES (3,4,GETDATE(),70000,'dadat')

--insert vao dbo.Bill
INSERT INTO dbo.Bill(idCustomerBooking,datePayment,totalPrice)	
VALUES (1,GETDATE(),100000)
INSERT INTO dbo.Bill(idCustomerBooking,datePayment,totalPrice)		
VALUES (2,GETDATE(),200000)
INSERT INTO dbo.Bill(idCustomerBooking,datePayment,totalPrice)		
VALUES (2,GETDATE(),150000)



-- xử lí khi nhiều đối tượng phụ thuộc vào
ALTER TABLE dbo.CustomerBooking	
DROP CONSTRAINT DF__CustomerB__endTi__3E52440B;

-- xóa tất cả các value
DELETE FROM dbo.FieldName;
DELETE FROM dbo.Customer;
DELETE FROM dbo.CustomerBooking;
DELETE FROM dbo.Bill;
--Tắt ràng buộc khóa ngoại trên bảng "CustomerBooking"
ALTER TABLE CustomerBooking NOCHECK CONSTRAINT FK__CustomerB__idCus__3F466844;
ALTER TABLE CustomerBooking NOCHECK CONSTRAINT FK__CustomerB__idFie__403A8C7D;
ALTER TABLE dbo.Bill NOCHECK CONSTRAINT FK__Bill__idCustomer__440B1D61;
--Mở lại ràng buộc khóa ngoại
ALTER TABLE CustomerBooking WITH CHECK CHECK CONSTRAINT FK__CustomerB__idCus__3F466844;
