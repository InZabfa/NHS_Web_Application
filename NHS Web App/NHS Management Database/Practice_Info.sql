CREATE TABLE [dbo].[Practice_Info]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(125) NULL, 
    [Address] NVARCHAR(175) NULL, 
    [Phone_Number] NVARCHAR(12) NULL, 
    [Email] NVARCHAR(254) NULL
)
