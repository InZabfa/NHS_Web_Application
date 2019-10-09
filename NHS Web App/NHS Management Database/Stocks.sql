CREATE TABLE [dbo].[Stocks]
(
	[MedicineID] INT UNIQUE NOT NULL, 
    [Quantity] INT NOT NULL, 
    CONSTRAINT [FK_Stock_MedicineID] FOREIGN KEY ([MedicineID]) REFERENCES [Medications]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [PK_Stocks] PRIMARY KEY ([MedicineID]) 
)
