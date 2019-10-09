CREATE TABLE [dbo].[Logs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AssociatedUserID] INT NOT NULL, 
    [LoggedDate_Time] DATETIME NOT NULL DEFAULT GetDate(), 
    [Type] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_Logs_AssociatedUserID] FOREIGN KEY ([AssociatedUserID]) REFERENCES [Users]([Id]) ON DELETE CASCADE  
)
