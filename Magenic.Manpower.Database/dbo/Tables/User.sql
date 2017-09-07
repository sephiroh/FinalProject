CREATE TABLE [dbo].[User] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Email]     NVARCHAR (100) NOT NULL,
    [Firstname] NVARCHAR (100) NOT NULL,
    [Lastname]  NVARCHAR (100) NOT NULL,
    [PasswordHash]  Binary (32)  NOT NULL,
	[Salt] Binary(16) NOT NULL,
    [RoleId]    INT            NULL,
    [ContactNo] NVARCHAR (20)  NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170117-180429]
    ON [dbo].[User]([Email] ASC);

