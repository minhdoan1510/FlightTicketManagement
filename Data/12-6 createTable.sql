-- Create a new database called 'DataStore'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'DataStore'
)
CREATE DATABASE DataStore
GO

CREATE TABLE Account
(
    AccountId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    AccountUser [NVARCHAR](50) NOT NULL,
    AccountPass [NVARCHAR](50) NOT NULL
    -- specify more columns here
);
GO


CREATE TABLE Profile
(
    ProfileId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    ProfileName [NVARCHAR](50) NOT NULL,
    ProfileTypeID [int] NOT NULL,
    -- specify more columns here
);
GO

CREATE TABLE Permission
(
    PermissionId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    PermissionName [NVARCHAR](50) NOT NULL
    -- specify more columns here
);
GO


CREATE TABLE Per_Acc
(
    PerID [VARCHAR](10) NOT NULL, -- primary key column
    AccId [VARCHAR](10) NOT NULL, -- primary key column
	PRIMARY KEY (PerID, AccId)
    -- specify more columns here
);
GO

CREATE TABLE PerDetail
(
    PerDetailId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    code_action [NVARCHAR](50) NOT NULL
    -- specify more columns here
);
GO


CREATE TABLE Passenger
(
    PassengerId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    PassengerName [NVARCHAR](50) NOT NULL,
    PassengerIDCard [NVARCHAR](50) NOT NULL,
    PassenserTel [NVARCHAR](50) NOT NULL

    -- specify more columns here
);
GO


CREATE TABLE Ticket
(
    TicketId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    TicketIDPassenger [VARCHAR](10) NOT NULL,
    TicketIDBeginPoint int not NULL,
    TicketIDEndPoint int not NULL,

    -- specify more columns here
);
GO

CREATE TABLE City
(
    CityId [VARCHAR](10) NOT NULL PRIMARY KEY, -- primary key column
    CityName [NVARCHAR](50) NOT NULL
    -- specify more columns here
);
GO


ALTER TABLE dbo.Profile ADD CONSTRAINT Profile_FK FOREIGN KEY (ProfileId) REFERENCES dbo.Account(AccountId);
GO

ALTER TABLE dbo.Profile ADD CONSTRAINT Profile_FK FOREIGN KEY (ProfileId) REFERENCES dbo.Account(AccountId);
GO

ALTER TABLE dbo.Per_Acc ADD CONSTRAINT Per_Acc_FK FOREIGN KEY (PerID) REFERENCES dbo.Permission(PermissionId);
GO

ALTER TABLE dbo.Per_Acc ADD CONSTRAINT Per_Acc1_FK FOREIGN KEY (AccId) REFERENCES dbo.Profile(ProfileId);
GO

ALTER TABLE dbo.Ticket ADD CONSTRAINT Ticket_FK FOREIGN KEY (TicketIDPassenger) REFERENCES dbo.Passenger(PassengerId);
GO


INSERT dbo.Account
(
    AccountId,
    AccountUser,
    AccountPass
)
VALUES
(   '18521092',  -- AccountId - varchar(10)
    N'nkoxway49', -- AccountUser - nvarchar(50)
    N'1'  -- AccountPass - nvarchar(50)
    )

INSERT dbo.Profile
(
    ProfileId,
    ProfileName
)
VALUES
(   '18521092',  -- ProfileId - varchar(10)
    N'Minh Đoàn' -- ProfileName - nvarchar(50)
)

INSERT dbo.Permission
(
    PermissionId,
    PermissionName
)
VALUES
(   '0', -- PermissionId - varchar(10)
    N'Admin' -- PermissionName - nvarchar(50)
)

INSERT dbo.Permission
(
    PermissionId,
    PermissionName
)
VALUES
(   '1', -- PermissionId - varchar(10)
    N'Sale' -- PermissionName - nvarchar(50)
)

INSERT dbo.Per_Acc
(
    PerID,
    AccId
)
VALUES
(   '1', -- PerID - varchar(10)
    '18521092'  -- AccId - varchar(10)
)

CREATE PROCEDURE ProcSignup
	@id varchar(10),
	@user nvarchar(50),
	@pass nvarchar(50),
	@name nvarchar(50),
	@acctype int
AS
BEGIN
	DECLARE @countAcc INT;

	SELECT @countAcc = COUNT(*) FROM dbo.Account WHERE AccountUser = @user ;

	IF @countAcc = 0
		BEGIN
			INSERT dbo.Account
			(
				AccountId,
				AccountUser,
				AccountPass
			)
			VALUES
			(   @id,  -- AccountId - varchar(10)
				@user, -- AccountUser - nvarchar(50)
				@pass  -- AccountPass - nvarchar(50)
				)

			INSERT dbo.Profile
			(
				ProfileId,
				ProfileName
			)
			VALUES
			(   
				@id,  -- ProfileId - varchar(10)
				@name -- ProfileName - nvarchar(50)
			)

			INSERT dbo.Per_Acc
			(
				PerID,
				AccId
			)
			VALUES
			(   
				@acctype, -- PerID - varchar(10)
				@id  -- AccId - varchar(10)
			)
		END
	ELSE
		BEGIN
			RAISERROR('The username already exists',16,1)
		END
    
END;


CREATE PROCEDURE ProcLogin
	@user nvarchar(50),
	@pass nvarchar(50)
AS
BEGIN
	DECLARE @ID varchar(10);
	DECLARE @PerID INT;
	DECLARE @name nvarchar(50);
	SELECT @PerID = -1;

	SELECT @ID = AccountId, @PerID = PerID , @name = ProfileName 
	FROM dbo.Account,dbo.Per_Acc, dbo.Profile 
	WHERE AccountUser = @user AND AccountPass = @pass AND AccId = AccountId AND AccId = ProfileId;

	IF @PerID = -1
		BEGIN
			RAISERROR('Incorrect information',16,1)
		END
	ELSE
		SELECT @ID AS ID , @PerID AS TypeAccount, @name AS Name;
END;


EXECUTE ProcSignup '18520192', 'test','a', 'Test' ,1 -- @id	@user @pass @name @acctype
EXECUTE ProcLogin 'test','a' -- @user @pass
