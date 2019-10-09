CREATE TABLE [dbo].[Patient_Medications]
(
	[PatientID] INT NOT NULL, 
    [MedicationID] INT NOT NULL, 
	[StaffID] INT NOT NULL, 
    [Date_Time] DATETIME NOT NULL DEFAULT GetDate(), 
    [Note] NVARCHAR(MAX) NULL, 
    [Dosage_Per_Day] INT NULL, 
    [Dosage_Per_Week] INT NULL, 
    CONSTRAINT [FK_Patient_Medications_ConditionsID] FOREIGN KEY ([MedicationID]) REFERENCES [Medications]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Patient_Medications_PatientID] FOREIGN KEY ([PatientID]) REFERENCES [Users]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_Patient_Medications_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [Staff]([Id])
)