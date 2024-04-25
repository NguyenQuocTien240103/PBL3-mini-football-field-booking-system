
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
VALUES ('A','trong',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('B','trong',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('C','trong',1)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('A','trong',2)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('B','trong',2)
INSERT INTO dbo.FieldName(name,status,idFieldType)	
VALUES ('C','trong',2)

Update dbo.FieldName
set status='empty'
where status ='trong'
Select * from dbo.FieldName

alter table dbo.FieldType
add TypeName NVARCHAR(100)

alter table dbo.FieldType
drop column TypeName;

Update dbo.FieldType
set TypeName= 'san 7'
where id = 2


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