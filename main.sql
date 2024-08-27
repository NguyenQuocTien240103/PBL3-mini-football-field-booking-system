CREATE TABLE FieldType(
	id INT IDENTITY PRIMARY KEY,
	TypeName NVARCHAR(100) NOT NULL,
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
	startTime DATETIME NOT NULL,
	endTime	DATETIME NOT NULL,
	priceBooking FLOAT NOT NULL,
	status NVARCHAR(100) NOT NULL,
	ngaydat DateTime NOT NULL,
	FOREIGN KEY (idCustomer) REFERENCES dbo.Customer(id),
	FOREIGN KEY (idFieldName) REFERENCES dbo.FieldName(id),
)
CREATE TABLE Bill(
	id INT IDENTITY PRIMARY KEY,
	idCustomerBooking INT NOT NULL,
	totalPrice FLOAT NOT NULL,
	paymentDay DATE NOT NULL,
	FOREIGN KEY (idCustomerBooking) REFERENCES dbo.CustomerBooking(id),
)

INSERT INTO dbo.FieldType(TypeName,normalDayPrice,specialDayPrice)	
VALUES ('san 5',50000,100000)
INSERT INTO dbo.FieldType(TypeName,normalDayPrice,specialDayPrice)	
VALUES ('san 7',100000,150000)

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
VALUES ('C','empty',2)