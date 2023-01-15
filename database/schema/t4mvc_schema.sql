/*
	DROP TABLE [Note];
	DROP TABLE [Contact];
	DROP TABLE [Account];
*/

IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Account')
BEGIN 
	CREATE TABLE Account ( 
		AccountId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		Name varchar(255) NOT NULL,
		Address varchar(255) NULL,
		Address2 varchar(255) NULL,
		City varchar(64) NULL,
		State varchar(64) NULL,
		Zip varchar(16) NULL,
		Phone varchar(32) NULL,
		Fax varchar(32) NULL,
		Website varchar(255) NULL,
		ParentAccountId uniqueidentifier NULL,
		Lat float NULL,
		Lng float NULL,
		Description varchar(MAX) NULL,
		Active bit NULL,
	)

	CREATE INDEX IX_Account_Name ON Account(Name)
END 
IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Contact')
BEGIN 
	CREATE TABLE Contact ( 
		ContactId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		FirstName varchar(255) NULL,
		LastName varchar(255) NULL,
		AccountId uniqueidentifier NULL CONSTRAINT FK_Contact_AccountId FOREIGN KEY REFERENCES Account(AccountId),
		MiddleName varchar(128) NULL,
		Prefix varchar(64) NULL,
		Suffix varchar(64) NULL,
		EmailAddress varchar(255) NULL,
		JobTitle varchar(255) NULL,
		Phone varchar NULL,
		Fax varchar(32) NULL,
		Mobile varchar(32) NULL,
		Address varchar(255) NULL,
		Address2 varchar(128) NULL,
		City varchar(64) NULL,
		State varchar(64) NULL,
		Zip varchar(16) NULL,
		Active bit NULL,
	)

	CREATE INDEX IX_Contact_FirstName ON Contact(FirstName)
	CREATE INDEX IX_Contact_LastName ON Contact(LastName)
	CREATE INDEX IX_Contact_AccountId ON Contact(AccountId)
	CREATE INDEX IX_Contact_EmailAddress ON Contact(EmailAddress)
END 
IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Note')
BEGIN 
	CREATE TABLE Note ( 
		NoteId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		NoteText varchar NOT NULL,
		AccountId uniqueidentifier NULL CONSTRAINT FK_Note_AccountId FOREIGN KEY REFERENCES Account(AccountId),
		ContactId uniqueidentifier NULL CONSTRAINT FK_Note_ContactId FOREIGN KEY REFERENCES Contact(ContactId),
	)

	CREATE INDEX IX_Note_AccountId ON Note(AccountId)
	CREATE INDEX IX_Note_ContactId ON Note(ContactId)
END 

