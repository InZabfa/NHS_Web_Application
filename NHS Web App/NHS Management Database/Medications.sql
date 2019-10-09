CREATE TABLE [dbo].[Medications]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [ProviderID] INT NOT NULL, 
    [Date_Added] DATETIME NOT NULL DEFAULT GetDate(), 
    [Added_By_StaffID] INT NOT NULL, 
    [Max_Dosage_Per_Day] INT NOT NULL DEFAULT 2, 
    [Max_Dosage_Per_Week] INT NOT NULL DEFAULT 3, 
    CONSTRAINT [FK_Medications_Provider_Info] FOREIGN KEY ([ProviderID]) REFERENCES [Provider_Info]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_Medications_AddedStaffID] FOREIGN KEY ([Added_By_StaffID]) REFERENCES [Staff]([Id]), 
)
