CREATE TABLE [dbo].[Appointments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Appointment_DateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    [Appointment_Duration_Minutes] INT NOT NULL DEFAULT 15, 
    [StaffID] INT NOT NULL, 
    [Ref_Number] NVARCHAR(10) NOT NULL, 
    [Room] NVARCHAR(10) NOT NULL, 
    [PatientUserID] INT NOT NULL, 
    CONSTRAINT [FK_Appointments_PatientID] FOREIGN KEY ([PatientUserID]) REFERENCES [Patients]([UserID]) ON DELETE CASCADE,
	CONSTRAINT [FK_Appointments_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [Staff]([Id]) ON DELETE CASCADE
)

GO
