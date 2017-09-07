CREATE TABLE [dbo].[Project]
(
	[Id]			INT IDENTITY(1,1) NOT NULL, 
    [Name]		    VARCHAR(50) NOT NULL, 
    [Description]	NVARCHAR(MAX) NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [DateCreated]	DATETIME NOT NULL, 
    [DateUpdated]	DATETIME NULL,
	CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC)
); 
