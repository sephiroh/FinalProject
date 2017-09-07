CREATE TABLE [dbo].[PrimarySkill] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [DateCreated] DATETIME       NOT NULL,
    [DateUpdated] DATETIME       NULL,
    [IsActive]    BIT            NOT NULL,
    CONSTRAINT [PK_PrimarySkill] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20170117-175933]
    ON [dbo].[PrimarySkill]([Name] ASC);

