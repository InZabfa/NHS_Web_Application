CREATE TABLE [dbo].[Staff]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserID] INT NOT NULL,
	[Contract_type] NVARCHAR(30) NOT NULL,
	[Working_days] NVARCHAR(7) NOT NULL DEFAULT '1111100' , 
    [Working_hours] INT NOT NULL DEFAULT 40, 
    [Staff_Role] NVARCHAR(30) NOT NULL,
	[FT_PT] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Staff_ToUserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([Id]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of contract',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Staff',
    @level2type = N'COLUMN',
    @level2name = N'Contract_type'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Represented as binary, (Mon,Tue,Wed,Thu,Fri,Sat,Sun) by default all days apart from Sat and Sun',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Staff',
    @level2type = N'COLUMN',
    @level2name = N'Working_days'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Number of hours contracted to work',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Staff',
    @level2type = N'COLUMN',
    @level2name = N'Working_hours'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Role of staff',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Staff',
    @level2type = N'COLUMN',
    @level2name = N'Staff_Role'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 if full-time, 0 if part-time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Staff',
    @level2type = N'COLUMN',
    @level2name = 'FT_PT'