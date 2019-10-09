CREATE TABLE [dbo].[Conditions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(200) NOT NULL,
	[Additional_Info] NVARCHAR(MAX), 
    [Added_By_StaffID] INT NOT NULL, 
    [Date_Added] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_Conditions_StaffID] FOREIGN KEY ([Added_By_StaffID]) REFERENCES [Staff]([Id])
)
