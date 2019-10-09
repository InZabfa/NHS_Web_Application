CREATE TABLE [dbo].[Provider_Info]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(70) NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [Phone_Number] NVARCHAR(30) NOT NULL, 
    [Email_Address] NVARCHAR(254) NOT NULL,
)
