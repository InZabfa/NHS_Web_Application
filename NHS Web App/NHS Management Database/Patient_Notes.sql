CREATE TABLE [dbo].[Patient_Notes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StaffID] INT NOT NULL, 
    [Added_DateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [Lowest_Access_Level_Required] INT NOT NULL DEFAULT 3, 
    [UserID] INT NOT NULL, 
    [Note] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_Patient_Notes_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [Staff]([Id]), 
	CONSTRAINT [FK_Patient_Notes_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [CK_Patient_Notes_Lowest_Access_Level_Required] CHECK ([Lowest_Access_Level_Required] <= 10)
)
