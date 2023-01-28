# Notes:
# Notes:
# To specify data type on a field, use parens. Eg. UserId(Guid)
# to make something nullable, include a ? after the name. Eg. Username?
# If the data type is a value type, you must also include the question mark on the data type. Eg. UserId?(Guid?)
# Entity attributes
#	- Area			= The mvc area
#	- Icon			= Icon to be used
#	- DontScaffold	= Don't create the admin CRUD functionality. This will still create the models, services, ViewModelServices etc...
#	- Description	= Override the name field. This can be useful for spaces or characters not allowed in a class name (')
#	- Plural		= Override the plural name. By default plural adds an s to the end. But that doesn't make sense for something like Installed Software (which is already plural) or categoy (categories)
#	- RawData		= Don't include the audit fields. Also - no edit button
#	- NoNav			= Don't add to the main navigation bar
#	- Security		= Adds role based permission checks
#	- EnableAuditing= Adds create/update/delete auditing and old value/new value tracking for the entity. This logs to the AuditRecord table
#
# Viewmodel attributes can be
#	- TextArea		= [DataType(DataType.MultilineText)]
#	- AllowHtml		= [AllowHtml]
#	- Wysiwyg		= [UIHint("t4mvcWysiwyg")]
#	- Money			= [UIHint("Money")]
#	- Phone			= [UIHint("Phone")]
#	- Email			= [UIHint("Email")]
#	- Website		= [UIHint("Website")]
#	- Date			= [DataType(DataType.Date)]
#
# Field attributes are
#	- Description	= ViewModel Description Attribute
#	- GridExclude	= Don't include on the grid
#	- IsNameField	= Lookup identifier
#	- IsSearchable	= Should this field be searchable
#	- SearchOperator= What operator should be used for searching. E.g. SearchOperator:Equals on a FileName field will translate to FileName.Equals(). The default is StartsWith
#	- IsIndex		= Not sure this is used
#	- Declassify	= NEED DOCUMENTATION
#	- RenderFunction= A formatting function that should be applied to the field. This will run on a data grid, and in the details view.
#	- Prefetch		= Only applicable to reference fields. Indicates whether the dropdown list should pre-load data vs using ajax api
#	- ProcessFunction = Override the function which processes the data into Select2 data format. This is a gxi function with the gxi prefix.
#	- NoReference	= NEEDS DOCUMENTATION
Account | Area:crm| Icon:feather-home | Security(crm) | EnableAuditing
	Name | IsIndexed | IsSearchable | KeyField | IsNameField | SearchOperator:Contains | Length:255
	Address? | Length:255
	Address2? | Length:255
	City? | Length:64
	State? | Length:64
	Zip? | Length:16
	Phone? | ViewModelAttributes(Phone) | RenderFunction:t4mvc.formatPhoneNumber | Length:32
	Fax? | Length:32
	Website? | ViewModelAttributes(Website) | Length:255
	ParentAccountId?(Guid?) | Description:Parent Account
	Lat?(double?)
	Lng?(double?)
	Description? | ViewModelAttributes(Wysiwyg) | Length:-1
	Active?(bool?)

Contact | Area:crm | Icon:feather-user | HasNotes
	First Name? | IsSearchable | Length:255
	Last Name? | IsSearchable | Length:255
	AccountId?(Guid?) | Description: Account | References Account(AccountId):Name Tabbed
	Middle Name? | Description: Middle | GridExclude | Length:128
	Prefix?	| GridExclude | Length:64
	Suffix?		| GridExclude | Length:64
	Email Address? | IsIndexed | IsSearchable | KeyField | IsNameField | ViewModelAttributes(Email) | Length:255
	Job Title? | Length:255
	Phone? | ViewModelAttributes(Phone) | RenderFunction:t4mvc.formatPhoneNumber
	Fax?		| GridExclude | Length:32
	Mobile? | GridExclude | ViewModelAttributes(Phone) | Length:32
	Address? | GridExclude | Length:255
	Address2? | GridExclude | Length:128
	City? | GridExclude | Length:64
	State? | GridExclude | Length:64
	Zip? | GridExclude | Length:16
	Active?(bool)

Project | Area:consulting | Icon:feather-archive | HasNotes | EnableAuditing
	Project Name | IsSearchable | Length:255 | IsNameField
	Start Date?(DateTime?) | ViewModelAttributes(Date) | RenderFunction:t4mvc.formatDate
	Due Date?(DateTime?) | ViewModelAttributes(Date) | RenderFunction:t4mvc.formatDate
	AccountId?(Guid?) | Description: Account | References Account(AccountId):Name Tabbed | IsSearchable | SearchOperator:Contains
	PrimaryContactId?(Guid?) | Description: Primary Contact | References Contact(ContactId):EmailAddress Tabbed | IsSearchable | SearchOperator:Contains
	Description? | ViewModelAttributes(Wysiwyg) | Length:-1
	EstimatedIncome?(decimal?) | Description: Estimated Income | ViewModelAttributes(Money) | Length:10 | Scale:2 | RenderFunction:t4mvc.formatMoney

Note | DontScaffold | Declassify(ModifyDate, ModifyUserId)
	NoteText | ViewModelAttributes(TextArea, Wysiwyg) | Length:-1
	AccountId?(Guid?) | References Account(AccountId)
	ContactId?(Guid?) | References Contact(ContactId)
	ProjectId?(Guid?) | References Project(ProjectId)