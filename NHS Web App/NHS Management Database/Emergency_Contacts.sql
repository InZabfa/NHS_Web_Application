﻿CREATE TABLE [dbo].[Emergency_Contacts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Forename] NVARCHAR(50) NOT NULL,
	[Surname] NVARCHAR(70) NOT NULL,
	[Address] NVARCHAR(175) NOT NULL,
	[Phone_Number] NVARCHAR(12) NOT NULL,
	[Relation] NVARCHAR(20) NOT NULL, 
    [UserID] INT NOT NULL, 
    CONSTRAINT [FK_Emergency_Contacts_UsersId] FOREIGN KEY ([UserID]) REFERENCES [Users]([Id]) ON DELETE CASCADE
)
