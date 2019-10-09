CREATE TABLE [dbo].[Patient_Conditions]
(
	[Id] INT IDENTITY NOT NULL, 
	[PatientID] INT NOT NULL, 
    [ConditionID] INT NOT NULL, 
	[StaffID] INT NOT NULL, 
    [Date_Time] DATETIME NOT NULL DEFAULT GetDate(), 
    [Note] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Conditions_ConditionsID] FOREIGN KEY ([ConditionID]) REFERENCES [Conditions]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Conditions_PatientID] FOREIGN KEY ([PatientID]) REFERENCES [Users]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_Patients_Conditions_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [Staff]([Id]), 
    CONSTRAINT [PK_Patient_Conditions] PRIMARY KEY ([Id])
)

GO
