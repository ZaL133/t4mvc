IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Account')
BEGIN 
	CREATE TABLE Account ( 
		AccountId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		Name varchar NOT NULL,
		Address varchar NULL,
		Address2 varchar NULL,
		City varchar NULL,
		State varchar NULL,
		Zip varchar NULL,
		Phone varchar NULL,
		Fax varchar NULL,
		Website varchar NULL,
		ParentAccountId uniqueidentifier NULL,
		Lat float NULL,
		Lng float NULL,
		Description varchar NULL,
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
		FirstName varchar NULL,
		LastName varchar NULL,
		AccountId uniqueidentifier NULL CONSTRAINT FK_Contact_AccountId FOREIGN KEY REFERENCES Account(AccountId),
		MiddleName varchar NULL,
		Prefix varchar NULL,
		Suffix varchar NULL,
		EmailAddress varchar NULL,
		JobTitle varchar NULL,
		Phone varchar NULL,
		Fax varchar NULL,
		Mobile varchar NULL,
		Address varchar NULL,
		Address2 varchar NULL,
		City varchar NULL,
		State varchar NULL,
		Zip varchar NULL,
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

