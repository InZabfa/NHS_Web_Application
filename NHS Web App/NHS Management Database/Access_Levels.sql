CREATE TABLE [dbo].[Access_Levels]
(
	[UserID] INT NOT NULL PRIMARY KEY UNIQUE, 
    [Access_Level] INT NOT NULL DEFAULT 1, 
    [Access_Enabled] BIT NOT NULL DEFAULT 1, 
    [Disabled_By_StaffID] INT NULL, 
    CONSTRAINT [CK_Access_level] CHECK ([Access_Level] <= 10 AND [Access_Level] >= 0), 
    CONSTRAINT [FK_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([Id]) ON DELETE CASCADE
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Access level, by default 1',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Access_Levels',
    @level2type = N'COLUMN',
    @level2name = N'Access_Level'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'By default, enabled',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Access_Levels',
    @level2type = N'COLUMN',
    @level2name = N'Access_Enabled'