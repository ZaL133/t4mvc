/*
	DROP TABLE [Note];
	DROP TABLE [Invoice];
	DROP TABLE [ProjectLog];
	DROP TABLE [Project];
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
END 

-- Field: Account.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'AccountId')	
		ALTER TABLE Account ADD AccountId uniqueidentifier NOT NULL PRIMARY KEY 

-- Field: Account.CreateUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'CreateUserId')	
		ALTER TABLE Account ADD CreateUserId uniqueidentifier NOT NULL

-- Field: Account.CreateDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'CreateDate')	
		ALTER TABLE Account ADD CreateDate datetime NOT NULL

-- Field: Account.ModifyUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'ModifyUserId')	
		ALTER TABLE Account ADD ModifyUserId uniqueidentifier NOT NULL

-- Field: Account.ModifyDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'ModifyDate')	
		ALTER TABLE Account ADD ModifyDate datetime NOT NULL

-- Field: Account.Name
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Name')	
		ALTER TABLE Account ADD Name varchar(255) NOT NULL

-- Field: Account.Address
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Address')	
		ALTER TABLE Account ADD Address varchar(255) NULL

-- Field: Account.Address2
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Address2')	
		ALTER TABLE Account ADD Address2 varchar(255) NULL

-- Field: Account.City
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'City')	
		ALTER TABLE Account ADD City varchar(64) NULL

-- Field: Account.State
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'State')	
		ALTER TABLE Account ADD State varchar(64) NULL

-- Field: Account.Zip
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Zip')	
		ALTER TABLE Account ADD Zip varchar(16) NULL

-- Field: Account.Phone
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Phone')	
		ALTER TABLE Account ADD Phone varchar(32) NULL

-- Field: Account.Fax
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Fax')	
		ALTER TABLE Account ADD Fax varchar(32) NULL

-- Field: Account.Website
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Website')	
		ALTER TABLE Account ADD Website varchar(255) NULL

-- Field: Account.ParentAccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'ParentAccountId')	
		ALTER TABLE Account ADD ParentAccountId uniqueidentifier NULL

-- Field: Account.Lat
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Lat')	
		ALTER TABLE Account ADD Lat float NULL

-- Field: Account.Lng
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Lng')	
		ALTER TABLE Account ADD Lng float NULL

-- Field: Account.Description
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Description')	
		ALTER TABLE Account ADD Description varchar(MAX) NULL

-- Field: Account.Active
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Account' AND c.[name] = 'Active')	
		ALTER TABLE Account ADD Active bit NULL

-- Index: Account.Name
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Account' AND i.[name] = 'IX_Account_Name')
		CREATE INDEX IX_Account_Name ON Account(Name)

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
END 

-- Field: Contact.ContactId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'ContactId')	
		ALTER TABLE Contact ADD ContactId uniqueidentifier NOT NULL PRIMARY KEY 

-- Field: Contact.CreateUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'CreateUserId')	
		ALTER TABLE Contact ADD CreateUserId uniqueidentifier NOT NULL

-- Field: Contact.CreateDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'CreateDate')	
		ALTER TABLE Contact ADD CreateDate datetime NOT NULL

-- Field: Contact.ModifyUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'ModifyUserId')	
		ALTER TABLE Contact ADD ModifyUserId uniqueidentifier NOT NULL

-- Field: Contact.ModifyDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'ModifyDate')	
		ALTER TABLE Contact ADD ModifyDate datetime NOT NULL

-- Field: Contact.FirstName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'FirstName')	
		ALTER TABLE Contact ADD FirstName varchar(255) NULL

-- Field: Contact.LastName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'LastName')	
		ALTER TABLE Contact ADD LastName varchar(255) NULL

-- Field: Contact.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'AccountId')	
		ALTER TABLE Contact ADD AccountId uniqueidentifier NULL CONSTRAINT FK_Contact_AccountId FOREIGN KEY REFERENCES Account(AccountId)

-- Field: Contact.MiddleName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'MiddleName')	
		ALTER TABLE Contact ADD MiddleName varchar(128) NULL

-- Field: Contact.Prefix
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Prefix')	
		ALTER TABLE Contact ADD Prefix varchar(64) NULL

-- Field: Contact.Suffix
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Suffix')	
		ALTER TABLE Contact ADD Suffix varchar(64) NULL

-- Field: Contact.EmailAddress
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'EmailAddress')	
		ALTER TABLE Contact ADD EmailAddress varchar(255) NULL

-- Field: Contact.JobTitle
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'JobTitle')	
		ALTER TABLE Contact ADD JobTitle varchar(255) NULL

-- Field: Contact.Phone
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Phone')	
		ALTER TABLE Contact ADD Phone varchar NULL

-- Field: Contact.Fax
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Fax')	
		ALTER TABLE Contact ADD Fax varchar(32) NULL

-- Field: Contact.Mobile
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Mobile')	
		ALTER TABLE Contact ADD Mobile varchar(32) NULL

-- Field: Contact.Address
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Address')	
		ALTER TABLE Contact ADD Address varchar(255) NULL

-- Field: Contact.Address2
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Address2')	
		ALTER TABLE Contact ADD Address2 varchar(128) NULL

-- Field: Contact.City
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'City')	
		ALTER TABLE Contact ADD City varchar(64) NULL

-- Field: Contact.State
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'State')	
		ALTER TABLE Contact ADD State varchar(64) NULL

-- Field: Contact.Zip
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Zip')	
		ALTER TABLE Contact ADD Zip varchar(16) NULL

-- Field: Contact.Active
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Contact' AND c.[name] = 'Active')	
		ALTER TABLE Contact ADD Active bit NULL

-- Index: Contact.FirstName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Contact' AND i.[name] = 'IX_Contact_FirstName')
		CREATE INDEX IX_Contact_FirstName ON Contact(FirstName)

-- Index: Contact.LastName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Contact' AND i.[name] = 'IX_Contact_LastName')
		CREATE INDEX IX_Contact_LastName ON Contact(LastName)

-- Index: Contact.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Contact' AND i.[name] = 'IX_Contact_AccountId')
		CREATE INDEX IX_Contact_AccountId ON Contact(AccountId)

-- Index: Contact.EmailAddress
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Contact' AND i.[name] = 'IX_Contact_EmailAddress')
		CREATE INDEX IX_Contact_EmailAddress ON Contact(EmailAddress)

IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Project')
BEGIN 
	CREATE TABLE Project ( 
		ProjectId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		ProjectName varchar(255) NOT NULL,
		StartDate datetime NULL,
		DueDate datetime NULL,
		AccountId uniqueidentifier NULL CONSTRAINT FK_Project_AccountId FOREIGN KEY REFERENCES Account(AccountId),
		PrimaryContactId uniqueidentifier NULL CONSTRAINT FK_Project_PrimaryContactId FOREIGN KEY REFERENCES Contact(ContactId),
		Description varchar(MAX) NULL,
		EstimatedIncome decimal(10,2) NULL,
	)
END 

-- Field: Project.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'ProjectId')	
		ALTER TABLE Project ADD ProjectId uniqueidentifier NOT NULL PRIMARY KEY 

-- Field: Project.CreateUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'CreateUserId')	
		ALTER TABLE Project ADD CreateUserId uniqueidentifier NOT NULL

-- Field: Project.CreateDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'CreateDate')	
		ALTER TABLE Project ADD CreateDate datetime NOT NULL

-- Field: Project.ModifyUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'ModifyUserId')	
		ALTER TABLE Project ADD ModifyUserId uniqueidentifier NOT NULL

-- Field: Project.ModifyDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'ModifyDate')	
		ALTER TABLE Project ADD ModifyDate datetime NOT NULL

-- Field: Project.ProjectName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'ProjectName')	
		ALTER TABLE Project ADD ProjectName varchar(255) NOT NULL

-- Field: Project.StartDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'StartDate')	
		ALTER TABLE Project ADD StartDate datetime NULL

-- Field: Project.DueDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'DueDate')	
		ALTER TABLE Project ADD DueDate datetime NULL

-- Field: Project.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'AccountId')	
		ALTER TABLE Project ADD AccountId uniqueidentifier NULL CONSTRAINT FK_Project_AccountId FOREIGN KEY REFERENCES Account(AccountId)

-- Field: Project.PrimaryContactId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'PrimaryContactId')	
		ALTER TABLE Project ADD PrimaryContactId uniqueidentifier NULL CONSTRAINT FK_Project_PrimaryContactId FOREIGN KEY REFERENCES Contact(ContactId)

-- Field: Project.Description
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'Description')	
		ALTER TABLE Project ADD Description varchar(MAX) NULL

-- Field: Project.EstimatedIncome
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Project' AND c.[name] = 'EstimatedIncome')	
		ALTER TABLE Project ADD EstimatedIncome decimal(10,2) NULL

-- Index: Project.ProjectName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Project' AND i.[name] = 'IX_Project_ProjectName')
		CREATE INDEX IX_Project_ProjectName ON Project(ProjectName)

-- Index: Project.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Project' AND i.[name] = 'IX_Project_AccountId')
		CREATE INDEX IX_Project_AccountId ON Project(AccountId)

-- Index: Project.PrimaryContactId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Project' AND i.[name] = 'IX_Project_PrimaryContactId')
		CREATE INDEX IX_Project_PrimaryContactId ON Project(PrimaryContactId)

IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'ProjectLog')
BEGIN 
	CREATE TABLE ProjectLog ( 
		ProjectLogId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		ProjectId uniqueidentifier NOT NULL CONSTRAINT FK_ProjectLog_ProjectId FOREIGN KEY REFERENCES Project(ProjectId),
		EntryName varchar(255) NOT NULL,
		EntryDate datetime NOT NULL,
		Hours decimal(10,2) NOT NULL,
	)
END 

-- Field: ProjectLog.ProjectLogId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'ProjectLogId')	
		ALTER TABLE ProjectLog ADD ProjectLogId uniqueidentifier NOT NULL PRIMARY KEY 

-- Field: ProjectLog.CreateUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'CreateUserId')	
		ALTER TABLE ProjectLog ADD CreateUserId uniqueidentifier NOT NULL

-- Field: ProjectLog.CreateDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'CreateDate')	
		ALTER TABLE ProjectLog ADD CreateDate datetime NOT NULL

-- Field: ProjectLog.ModifyUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'ModifyUserId')	
		ALTER TABLE ProjectLog ADD ModifyUserId uniqueidentifier NOT NULL

-- Field: ProjectLog.ModifyDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'ModifyDate')	
		ALTER TABLE ProjectLog ADD ModifyDate datetime NOT NULL

-- Field: ProjectLog.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'ProjectId')	
		ALTER TABLE ProjectLog ADD ProjectId uniqueidentifier NOT NULL CONSTRAINT FK_ProjectLog_ProjectId FOREIGN KEY REFERENCES Project(ProjectId)

-- Field: ProjectLog.EntryName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'EntryName')	
		ALTER TABLE ProjectLog ADD EntryName varchar(255) NOT NULL

-- Field: ProjectLog.EntryDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'EntryDate')	
		ALTER TABLE ProjectLog ADD EntryDate datetime NOT NULL

-- Field: ProjectLog.Hours
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'ProjectLog' AND c.[name] = 'Hours')	
		ALTER TABLE ProjectLog ADD Hours decimal(10,2) NOT NULL

-- Index: ProjectLog.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'ProjectLog' AND i.[name] = 'IX_ProjectLog_ProjectId')
		CREATE INDEX IX_ProjectLog_ProjectId ON ProjectLog(ProjectId)

-- Index: ProjectLog.EntryName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'ProjectLog' AND i.[name] = 'IX_ProjectLog_EntryName')
		CREATE INDEX IX_ProjectLog_EntryName ON ProjectLog(EntryName)

IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Invoice')
BEGIN 
	CREATE TABLE Invoice ( 
		InvoiceId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		ProjectId uniqueidentifier NOT NULL CONSTRAINT FK_Invoice_ProjectId FOREIGN KEY REFERENCES Project(ProjectId),
		InvoiceName varchar(255) NOT NULL,
		InvoiceDate datetime NOT NULL,
		InvoiceAmount decimal(10,2) NOT NULL,
		Status int NULL,
	)
END 

-- Field: Invoice.InvoiceId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'InvoiceId')	
		ALTER TABLE Invoice ADD InvoiceId uniqueidentifier NOT NULL PRIMARY KEY 

-- Field: Invoice.CreateUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'CreateUserId')	
		ALTER TABLE Invoice ADD CreateUserId uniqueidentifier NOT NULL

-- Field: Invoice.CreateDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'CreateDate')	
		ALTER TABLE Invoice ADD CreateDate datetime NOT NULL

-- Field: Invoice.ModifyUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'ModifyUserId')	
		ALTER TABLE Invoice ADD ModifyUserId uniqueidentifier NOT NULL

-- Field: Invoice.ModifyDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'ModifyDate')	
		ALTER TABLE Invoice ADD ModifyDate datetime NOT NULL

-- Field: Invoice.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'ProjectId')	
		ALTER TABLE Invoice ADD ProjectId uniqueidentifier NOT NULL CONSTRAINT FK_Invoice_ProjectId FOREIGN KEY REFERENCES Project(ProjectId)

-- Field: Invoice.InvoiceName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'InvoiceName')	
		ALTER TABLE Invoice ADD InvoiceName varchar(255) NOT NULL

-- Field: Invoice.InvoiceDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'InvoiceDate')	
		ALTER TABLE Invoice ADD InvoiceDate datetime NOT NULL

-- Field: Invoice.InvoiceAmount
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'InvoiceAmount')	
		ALTER TABLE Invoice ADD InvoiceAmount decimal(10,2) NOT NULL

-- Field: Invoice.Status
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Invoice' AND c.[name] = 'Status')	
		ALTER TABLE Invoice ADD Status int NULL

-- Index: Invoice.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Invoice' AND i.[name] = 'IX_Invoice_ProjectId')
		CREATE INDEX IX_Invoice_ProjectId ON Invoice(ProjectId)

-- Index: Invoice.InvoiceName
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Invoice' AND i.[name] = 'IX_Invoice_InvoiceName')
		CREATE INDEX IX_Invoice_InvoiceName ON Invoice(InvoiceName)

IF NOT EXISTS (
	SELECT * FROM sys.Tables WHERE [Name] = 'Note')
BEGIN 
	CREATE TABLE Note ( 
		NoteId uniqueidentifier NOT NULL PRIMARY KEY ,
		CreateUserId uniqueidentifier NOT NULL,
		CreateDate datetime NOT NULL,
		ModifyUserId uniqueidentifier NOT NULL,
		ModifyDate datetime NOT NULL,
		NoteText varchar(MAX) NOT NULL,
		AccountId uniqueidentifier NULL CONSTRAINT FK_Note_AccountId FOREIGN KEY REFERENCES Account(AccountId),
		ContactId uniqueidentifier NULL CONSTRAINT FK_Note_ContactId FOREIGN KEY REFERENCES Contact(ContactId),
		ProjectId uniqueidentifier NULL CONSTRAINT FK_Note_ProjectId FOREIGN KEY REFERENCES Project(ProjectId),
		ProjectLogId uniqueidentifier NULL CONSTRAINT FK_Note_ProjectLogId FOREIGN KEY REFERENCES ProjectLog(ProjectLogId),
	)
END 

-- Field: Note.NoteId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'NoteId')	
		ALTER TABLE Note ADD NoteId uniqueidentifier NOT NULL PRIMARY KEY 

-- Field: Note.CreateUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'CreateUserId')	
		ALTER TABLE Note ADD CreateUserId uniqueidentifier NOT NULL

-- Field: Note.CreateDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'CreateDate')	
		ALTER TABLE Note ADD CreateDate datetime NOT NULL

-- Field: Note.ModifyUserId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'ModifyUserId')	
		ALTER TABLE Note ADD ModifyUserId uniqueidentifier NOT NULL

-- Field: Note.ModifyDate
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'ModifyDate')	
		ALTER TABLE Note ADD ModifyDate datetime NOT NULL

-- Field: Note.NoteText
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'NoteText')	
		ALTER TABLE Note ADD NoteText varchar(MAX) NOT NULL

-- Field: Note.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'AccountId')	
		ALTER TABLE Note ADD AccountId uniqueidentifier NULL CONSTRAINT FK_Note_AccountId FOREIGN KEY REFERENCES Account(AccountId)

-- Field: Note.ContactId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'ContactId')	
		ALTER TABLE Note ADD ContactId uniqueidentifier NULL CONSTRAINT FK_Note_ContactId FOREIGN KEY REFERENCES Contact(ContactId)

-- Field: Note.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'ProjectId')	
		ALTER TABLE Note ADD ProjectId uniqueidentifier NULL CONSTRAINT FK_Note_ProjectId FOREIGN KEY REFERENCES Project(ProjectId)

-- Field: Note.ProjectLogId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.[name] = 'Note' AND c.[name] = 'ProjectLogId')	
		ALTER TABLE Note ADD ProjectLogId uniqueidentifier NULL CONSTRAINT FK_Note_ProjectLogId FOREIGN KEY REFERENCES ProjectLog(ProjectLogId)

-- Index: Note.AccountId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Note' AND i.[name] = 'IX_Note_AccountId')
		CREATE INDEX IX_Note_AccountId ON Note(AccountId)

-- Index: Note.ContactId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Note' AND i.[name] = 'IX_Note_ContactId')
		CREATE INDEX IX_Note_ContactId ON Note(ContactId)

-- Index: Note.ProjectId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Note' AND i.[name] = 'IX_Note_ProjectId')
		CREATE INDEX IX_Note_ProjectId ON Note(ProjectId)

-- Index: Note.ProjectLogId
	IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.indexes i ON t.object_id = i.object_id WHERE t.[Name] = 'Note' AND i.[name] = 'IX_Note_ProjectLogId')
		CREATE INDEX IX_Note_ProjectLogId ON Note(ProjectLogId)


