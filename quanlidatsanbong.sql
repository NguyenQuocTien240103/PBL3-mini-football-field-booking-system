
CREATE TABLE FieldType(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,

	price FLOAT NOT NULL
)
ALTER TABLE FieldType
ADD  specialDayPrice INT;
ALTER TABLE FieldType
DROP COLUMN price;

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
	idFieldType INT NOT NULL,
	idFieldName INT NOT NULL,
	startTime TIME NOT NULL,
	endTime TIME DEFAULT GETDATE(),
	priceBooking FLOAT NOT NULL,
	FOREIGN KEY (idCustomer) REFERENCES dbo.Customer(id),
	FOREIGN KEY (idFieldType) REFERENCES dbo.FieldType(id),
	FOREIGN KEY (idFieldName) REFERENCES dbo.FieldName(id),
)
EXEC sp_rename 'FK__CustomerB__idFie__5812160E', 'FK_CustomerBooking_idFieldType';
ALTER TABLE CustomerBooking
DROP CONSTRAINT FK_CustomerBooking_idFieldType;
ALTER TABLE CustomerBooking
DROP COLUMN idFieldType;

alter table CustomerBooking
ADD state NVARCHAR(50);

CREATE TABLE Bill(
	id INT IDENTITY PRIMARY KEY,
	idCustomerBooking INT NOT NULL,
	datePayment DATETIME NOT NULL DEFAULT GETDATE(),
	totalPrice FLOAT NOT NULL,
	FOREIGN KEY (idCustomerBooking) REFERENCES dbo.CustomerBooking(id),


)

CREATE TABLE InformationBooking(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idCustomerBooking INT NOT NULL,
	statusBooking NVARCHAR(100) NOT NULL,
	FOREIGN KEY (idCustomerBooking) REFERENCES dbo.CustomerBooking(id),
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
)

drop table InformationBooking