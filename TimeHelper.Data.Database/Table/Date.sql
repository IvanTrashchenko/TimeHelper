CREATE TABLE [dbo].[Date]
(
	[DateId] INT NOT NULL IDENTITY, 
    [DateName] NVARCHAR(256) NOT NULL, 
    [DateValue] DATE NOT NULL,
    CONSTRAINT [PK_Date] PRIMARY KEY ([DateId]),
    CONSTRAINT UC_DateName UNIQUE ([DateName])
)
