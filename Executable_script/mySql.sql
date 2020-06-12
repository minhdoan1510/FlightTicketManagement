-- Create a new database called 'DataStore'
-- Connect to the 'master' database to run this snippet
-- Create the new database if it does not exist already

CREATE DATABASE DataStore;

 
use DataStore;

CREATE TABLE Account
(
    AccountId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    AccountUser VARCHAR(50) NOT NULL,
    AccountPass VARCHAR(50) NOT NULL
    -- specify more columns here
);


CREATE TABLE Profile
(
    ProfileId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    ProfileName NVARCHAR(50) NOT NULL
    -- specify more columns here
);

CREATE TABLE Permission
(
    PermissionId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    PermissionName NVARCHAR(50) NOT NULL
    -- specify more columns here
);


CREATE TABLE Per_Acc
(
    PerID VARCHAR(10) NOT NULL, -- primary key column
    AccId VARCHAR(10) NOT NULL, -- primary key column
	PRIMARY KEY (PerID, AccId)
    -- specify more columns here
);

CREATE TABLE PerDetail
(
    PerDetailId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    code_action NVARCHAR(50) NOT NULL
    -- specify more columns here
);


CREATE TABLE Passenger
(
    PassengerId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    PassengerName NVARCHAR(50) NOT NULL,
    PassengerIDCard NVARCHAR(50) NOT NULL,
    PassenserTel NVARCHAR(50) NOT NULL

    -- specify more columns here
);


CREATE TABLE Ticket
(
    TicketId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    TicketIDPassenger VARCHAR(10) NOT NULL,
    TicketIDBeginPoint int not NULL,
    TicketIDEndPoint int not NULL

    -- specify more columns here
);

CREATE TABLE City
(
    CityId VARCHAR(10) NOT NULL PRIMARY KEY, -- primary key column
    CityName NVARCHAR(50) NOT NULL
    -- specify more columns here
);


ALTER TABLE Profile ADD CONSTRAINT Profile_FK FOREIGN KEY (ProfileId) REFERENCES Account(AccountId);
 

ALTER TABLE Per_Acc ADD CONSTRAINT Per_Acc_FK FOREIGN KEY (PerID) REFERENCES Permission(PermissionId);
 

ALTER TABLE Per_Acc ADD CONSTRAINT Per_Acc1_FK FOREIGN KEY (AccId) REFERENCES Profile(ProfileId);
 

ALTER TABLE Ticket ADD CONSTRAINT Ticket_FK FOREIGN KEY (TicketIDPassenger) REFERENCES Passenger(PassengerId);
 


INSERT into Account
(
    AccountId,
    AccountUser,
    AccountPass
)
Values
(   '18521092',  -- AccountId - varchar(10)
    N'nkoxway49', -- AccountUser - nvarchar(50)
    N'1'  -- AccountPass - nvarchar(50)
    )

INSERT into Profile
(
    ProfileId,
    ProfileName
)
values
(   '18521092',  -- ProfileId - varchar(10)
    N'Minh Đoàn' -- ProfileName - nvarchar(50)
)

INSERT into Permission
(
    PermissionId,
    PermissionName
)
values
(   '0', -- PermissionId - varchar(10)
    N'Admin' -- PermissionName - nvarchar(50)
)

INSERT into Permission
(
    PermissionId,
    PermissionName
)
values
(   '1', -- PermissionId - varchar(10)
    N'Sale' -- PermissionName - nvarchar(50)
)

INSERT into Per_Acc
(
    PerID,
    AccId
)
values
(   '1', -- PerID - varchar(10)
    '18521092'  -- AccId - varchar(10)
)

DELIMITER //

CREATE PROCEDURE ProcSignup (
	In p_id varchar(10),
	In p_user nvarchar(50),
	In p_pass nvarchar(50),
	In p_name nvarchar(50),
	In p_acctype int)
begin
	DECLARE v_countAcc int;
	SELECT COUNT(*) = v_countAcc FROM Account WHERE AccountUser = p_user ;
	IF v_countAcc = 0
		THEN
			INSERT into Account
			(
				AccountId,
				AccountUser,
				AccountPass
			)
			values
			(   p_id,  -- AccountId - varchar(10)
				p_user, -- AccountUser - nvarchar(50)
				p_pass  -- AccountPass - nvarchar(50)
				);

			INSERT into Profile
			(
				ProfileId,
				ProfileName
			)
			values
			(   
				p_id,  -- ProfileId - varchar(10)
				p_name -- ProfileName - nvarchar(50)
			);

			INSERT into Per_Acc
			(
				PerID,
				AccId
			)
			values
			(   
				p_acctype, -- PerID - varchar(10);
				p_id  -- AccId - varchar(10);
			);
		ELSE
			SIGNAL SQLSTATE '02000' SET MESSAGE_TEXT = 'The username already exists';
		END IF;
    
END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE ProcLogin (
	p_user nvarchar(50),
	p_pass nvarchar(50))
BEGIN
	DECLARE v_ID varchar(10);
	DECLARE v_PerID INT;
	DECLARE v_name nvarchar(50);
	SET v_PerID = -1;

	SELECT AccountId, PerID , ProfileName INTO v_ID, v_PerID, v_name 
	FROM Account,Per_Acc, Profile 
	WHERE AccountUser = p_user AND AccountPass = p_pass AND AccId = AccountId AND AccId = ProfileId;

	IF v_PerID = -1
		THEN
			SIGNAL SQLSTATE '02000' SET MESSAGE_TEXT = 'Incorrect information';
	ELSE
		SELECT v_ID AS ID , v_PerID AS TypeAccount, v_name AS Name;
	END IF;
END;
//

DELIMITER ;


CREATE TABLE Airport
(
	AirportId VARCHAR(10) NOT NULL PRIMARY KEY,
	AirportName NVARCHAR(50) NOT NULL,
	CityId VARCHAR(10) NOT NULL
);

CREATE TABLE Flight
(
	FlightId VARCHAR(10) NOT NULL PRIMARY KEY,
	Price DECIMAL(15,4) NOT NULL,
	OriginAP VARCHAR(10) NOT NULL,
	DestinationAP VARCHAR(10) NOT NULL,
	Date DATE NOT NULL,
	Time TIME(6) NOT NULL,
	TotalSeat INTEGER NOT NULL
);

CREATE TABLE Transit
(
	FlightId VARCHAR(10) NOT NULL,
	AirportId VARCHAR(10) NOT NULL,
	Duration TIME(6) NOT NULL,
	Note LONGTEXT NULL
);

CREATE TABLE Class
(
	ClassId VARCHAR(10) NOT NULL PRIMARY KEY,
	ClassName NVARCHAR(50) NOT NULL,
	PriceRatio DOUBLE NOT NULL
);

ALTER TABLE Ticket
DROP COLUMN TicketIDBeginPoint;

ALTER TABLE Ticket
DROP COLUMN TicketIDEndPoint;

ALTER TABLE Ticket
add FlightId VARCHAR(10) NOT NULL;
ALTER TABLE Ticket
add ClassId VARCHAR(10) NOT NULL;
ALTER TABLE Ticket
add Type INTEGER NOT NULL;

ALTER TABLE Flight
ADD CONSTRAINT Origin_FK FOREIGN KEY(OriginAP) REFERENCES Airport(AirportId);

ALTER TABLE Flight
ADD CONSTRAINT Dest_FK FOREIGN KEY(OriginAP) REFERENCES Airport(AirportId);

ALTER TABLE Transit
ADD CONSTRAINT Flight_FK FOREIGN KEY(FlightId) REFERENCES Flight(FlightId);

ALTER TABLE Transit
ADD CONSTRAINT Airport_FK FOREIGN KEY(AirportId) REFERENCES Airport(AirportId);
ALTER TABLE Airport
ADD CONSTRAINT City_FK FOREIGN KEY(CityId) REFERENCES City(CityId);
ALTER TABLE Ticket
ADD CONSTRAINT Flight_Ticket_FK FOREIGN KEY(FlightID) REFERENCES Flight(FlightId);
ALTER TABLE Ticket
ADD CONSTRAINT Class_FK FOREIGN KEY(ClassId) REFERENCES Class(ClassId);
ALTER TABLE Transit
ADD CONSTRAINT Flight_AP_PK PRIMARY KEY(FlightId,AirportId);
