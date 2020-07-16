CREATE TABLE [dbo].[AccountsWithIndex]
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Country] [nvarchar](100) NULL
	
	-- constraints
	CONSTRAINT [PK_AccountsWithIndex] PRIMARY KEY CLUSTERED (Id)	
)
GO

CREATE INDEX [IDX_AccountsWithIndex_Country] ON [dbo].[AccountsWithIndex] ([Country])
GO