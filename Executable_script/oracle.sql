-- Create a new database called 'DataStore'
-- Connect to the 'master' database to run this snippet
ALTER SESSION SET CURRENT_SCHEMA = master;
 
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'DataStore'
) THEN
CREATE DATABASE DataStore
END IF;
 

CREATE TABLE Account
(
    AccountId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    AccountUser NVARCHAR2(50) NOT NULL,
    AccountPass NVARCHAR2(50) NOT NULL
    -- specify more columns here
);


CREATE TABLE Profile
(
    ProfileId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    ProfileName NVARCHAR2(50) NOT NULL,
    ProfileTypeID Number(10) NOT NULL,
    -- specify more columns here
);

CREATE TABLE Permission
(
    PermissionId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    PermissionName NVARCHAR2(50) NOT NULL
    -- specify more columns here
);


CREATE TABLE Per_Acc
(
    PerID VARCHAR2(10) NOT NULL, -- primary key column
    AccId VARCHAR2(10) NOT NULL, -- primary key column
	PRIMARY KEY (PerID, AccId)
    -- specify more columns here
);

CREATE TABLE PerDetail
(
    PerDetailId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    code_action NVARCHAR2(50) NOT NULL
    -- specify more columns here
);


CREATE TABLE Passenger
(
    PassengerId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    PassengerName NVARCHAR2(50) NOT NULL,
    PassengerIDCard NVARCHAR2(50) NOT NULL,
    PassenserTel NVARCHAR2(50) NOT NULL

    -- specify more columns here
);


CREATE TABLE Ticket
(
    TicketId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    TicketIDPassenger VARCHAR2(10) NOT NULL,
    TicketIDBeginPoint number(10) not NULL,
    TicketIDEndPoint number(10) not NULL,

    -- specify more columns here
);

CREATE TABLE City
(
    CityId VARCHAR2(10) NOT NULL PRIMARY KEY, -- primary key column
    CityName NVARCHAR2(50) NOT NULL
    -- specify more columns here
);


ALTER TABLE dbo.Profile ADD CONSTRAINT Profile_FK FOREIGN KEY (ProfileId) REFERENCES Account(AccountId);
 

ALTER TABLE dbo.Per_Acc ADD CONSTRAINT Per_Acc_FK FOREIGN KEY (PerID) REFERENCES Permission(PermissionId);
 

ALTER TABLE dbo.Per_Acc ADD CONSTRAINT Per_Acc1_FK FOREIGN KEY (AccId) REFERENCES Profile(ProfileId);
 

ALTER TABLE dbo.Ticket ADD CONSTRAINT Ticket_FK FOREIGN KEY (TicketIDPassenger) REFERENCES Passenger(PassengerId);
 


INSERT dbo.Account
(
    AccountId,
    AccountUser,
    AccountPass
)
SELECT
    '18521092',  -- AccountId - varchar(10)
    N'nkoxway49', -- AccountUser - nvarchar(50)
    N'1'  -- AccountPass - nvarchar(50)
      FROM dual

INSERT dbo.Profile
(
    ProfileId,
    ProfileName,
)
SELECT
    '18521092',  -- ProfileId - varchar(10)
    N'Minh Đoàn' -- ProfileName - nvarchar(50)
  FROM dual

INSERT dbo.Permission
(
    PermissionId,
    PermissionName
)
SELECT
    '0', -- PermissionId - varchar(10)
    N'Admin' -- PermissionName - nvarchar(50)
  FROM dual

INSERT dbo.Permission
(
    PermissionId,
    PermissionName
)
SELECT
    '1', -- PermissionId - varchar(10)
    N'Sale' -- PermissionName - nvarchar(50)
  FROM dual

INSERT dbo.Per_Acc
(
    PerID,
    AccId
)
SELECT
    '1', -- PerID - varchar(10)
    '18521092'  -- AccId - varchar(10)
  FROM dual

CREATE OR REPLACE PROCEDURE ProcSignup (
	p_id varchar2,
	p_user nvarchar2(50),
	p_pass nvarchar2(50),
	p_name nvarchar2(50),
	p_acctype number)
AS
 v_countAcc NUMBER(10);
BEGIN

	SELECT COUNT(*) INTO v_countAcc FROM Account WHERE AccountUser = p_user ;

	IF v_countAcc = 0
		THEN
			INSERT dbo.Account
			(
				AccountId,
				AccountUser,
				AccountPass
			)
			SELECT
			    p_id,  -- AccountId - varchar(10)
				p_user, -- AccountUser - nvarchar(50)
				p_pass  -- AccountPass - nvarchar(50)
				  FROM dual

			INSERT dbo.Profile
			(
				ProfileId,
				ProfileName
			)
			SELECT
			    
				p_id,  -- ProfileId - varchar(10)
				p_name -- ProfileName - nvarchar(50)
			  FROM dual

			INSERT dbo.Per_Acc
			(
				PerID,
				AccId
			)
			SELECT
			    
				p_acctype, -- PerID - varchar(10)
				p_id  -- AccId - varchar(10)
			  FROM dual
	ELSE
			RAISERROR('The username already exists',16,1)
		END IF;
    
END
/;


CREATE OR REPLACE PROCEDURE ProcLogin (
	p_user nvarchar2(50),
	p_pass nvarchar2(50)), cur OUT SYS_REFCURSOR
AS
 v_ID varchar2(10);
 v_PerID NUMBER(10);
 v_name nvarchar2(50);
BEGIN
 v_PerID := -1 FROM dual;

	SELECT AccountId, PerID , ProfileName INTO v_ID, v_PerID, v_name 
	FROM Account,Per_Acc, Profile 
	WHERE AccountUser = p_user AND AccountPass = p_pass AND AccId = AccountId AND AccId = ProfileId;

	IF v_PerID = -1
		THEN
			RAISERROR('Incorrect information',16,1)
	ELSE
		OPEN cur FOR SELECT v_ID AS ID , v_PerID AS TypeAccount, v_name AS Name FROM dual;
	END IF;
END
/;
CREATE TABLE Airport
(
	AirportId VARCHAR2(10) NOT NULL PRIMARY KEY,
	AirportName NVARCHAR2(50) NOT NULL,
	CityId VARCHAR2(10) NOT NULL
);

CREATE TABLE Flight
(
	FlightId VARCHAR2(10) NOT NULL PRIMARY KEY,
	Price NUMBER NOT NULL,
	OriginAP VARCHAR2(10) NOT NULL,
	DestinationAP VARCHAR2(10) NOT NULL,
	Date DATE NOT NULL,
	Time TIMESTAMP NOT NULL,
	TotalSeat NUMBER(10) NOT NULL
);

CREATE TABLE Transit
(
	FlightId VARCHAR2(10) NOT NULL,
	AirportId VARCHAR2(10) NOT NULL,
	Duration TIMESTAMP NOT NULL,
	Note NCLOB NULL
);

CREATE TABLE Class
(
	ClassId VARCHAR2(10) NOT NULL PRIMARY KEY,
	ClassName NVARCHAR2(50) NOT NULL,
	PriceRatio BINARY_DOUBLE NOT NULL
);

ALTER TABLE dbo.Ticket
DROP COLUMN TicketIDBeginPoint

ALTER TABLE dbo.Ticket
DROP COLUMN TicketIDEndPoint

ALTER TABLE Ticket
add FlightId VARCHAR(10) NOT NULL
ALTER TABLE Ticket
add ClassId VARCHAR(10) NOT NULL
ALTER TABLE Ticket
add Type INTEGER NOT NULL

ALTER TABLE dbo.Flight
ADD CONSTRAINT Origin_FK FOREIGN KEY(OriginAP) REFERENCES Airport(AirportId)

ALTER TABLE dbo.Flight
ADD CONSTRAINT Dest_FK FOREIGN KEY(OriginAP) REFERENCES Airport(AirportId)

ALTER TABLE dbo.Transit
ADD CONSTRAINT Flight_FK FOREIGN KEY(FlightId) REFERENCES Flight(FlightId)

ALTER TABLE dbo.Transit
ADD CONSTRAINT Airport_FK FOREIGN KEY(AirportId) REFERENCES Airport(AirportId)
ALTER TABLE dbo.Airport
ADD CONSTRAINT City_FK FOREIGN KEY(CityId) REFERENCES City(CityId)
ALTER TABLE Ticket
ADD CONSTRAINT Flight_Ticket_FK FOREIGN KEY(FlightID) REFERENCES Flight(FlightId)
ALTER TABLE Ticket
ADD CONSTRAINT Class_FK FOREIGN KEY(ClassId) REFERENCES Class(ClassId)
ALTER TABLE dbo.Transit
ADD CONSTRAINT Flight_AP_PK PRIMARY KEY(FlightId,AirportId)
