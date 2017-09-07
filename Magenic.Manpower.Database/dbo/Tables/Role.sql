CREATE TABLE [dbo].[Role] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [DateCreated] DATETIME      NOT NULL,
    [DateUpdated] DATETIME      NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170117-180016]
    ON [dbo].[Role]([Name] ASC);

