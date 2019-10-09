CREATE TABLE [dbo].[Patients]
(
	[UserID] INT NOT NULL  UNIQUE, 
    [StaffID] INT NOT NULL, 
    [Email_Notifications] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Patients_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [Staff]([Id]),
	CONSTRAINT [FK_Patients_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([Id]) ON DELETE CASCADE,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([UserID]) 
)
