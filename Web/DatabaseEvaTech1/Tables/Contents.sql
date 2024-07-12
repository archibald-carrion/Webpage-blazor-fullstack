CREATE TABLE [dbo].[Contents]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[ContentType] VARCHAR(255) NOT NULL,
	[ContentName] VARCHAR(255) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	[CareerAcronym] VARCHAR(255) NULL,
	FOREIGN KEY (CareerAcronym) 
		REFERENCES Career(Acronym)
)
