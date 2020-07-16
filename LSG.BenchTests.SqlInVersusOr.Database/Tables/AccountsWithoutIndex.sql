CREATE TABLE [dbo].[AccountsWithoutIndex]
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Country] [nvarchar](100) NULL

	-- constraints
	CONSTRAINT [PK_AccountsWithoutIndex] PRIMARY KEY CLUSTERED (Id)
)