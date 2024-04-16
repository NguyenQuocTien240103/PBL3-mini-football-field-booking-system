
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

