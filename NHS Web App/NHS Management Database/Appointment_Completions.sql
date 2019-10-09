CREATE TABLE [dbo].[Appointment_Completion]
(
	[AppointmentID] INT NOT NULL PRIMARY KEY, 
    [Status] INT NOT NULL, 
    [Changed_On] DATETIME NOT NULL DEFAULT GetDate(), 
    [StaffID] INT NOT NULL, 
    CONSTRAINT [FK_Appointment_Completion_Appointment_ID] FOREIGN KEY ([AppointmentID]) REFERENCES [Appointments]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Appointment_Completion_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [Staff]([Id])
)
