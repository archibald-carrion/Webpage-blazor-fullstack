CREATE TABLE [dbo].[Career]
(
	[Acronym] VARCHAR(255) NOT NULL PRIMARY KEY, 
	[Description] VARCHAR(255) NOT NULL,
	[Name] VARCHAR(255) NOT NULL,
	[Duration] INT NOT NULL,
	[isSteamRelated] BIT NOT NULL,
	[percentageFemaleStudents] FLOAT NOT NULL,
	[isECCI] BIT NOT NULL
)
