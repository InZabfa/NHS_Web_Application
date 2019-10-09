﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Forename] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(70) NOT NULL,
	[Gender] BIT NOT NULL,
	[DOB] DATE NOT NULL,
	[Email] NVARCHAR(254) NOT NULL UNIQUE,
	[Password] NVARCHAR(MAX) NOT NULL,
	[Address] NVARCHAR(175) NOT NULL,
	[Phone_number] NVARCHAR(30) NOT NULL,
    [PracticeID] INT NOT NULL, 
	CONSTRAINT [FK_Users_PracticeID] FOREIGN KEY (PracticeID) REFERENCES [Practice_Info]([Id])
)