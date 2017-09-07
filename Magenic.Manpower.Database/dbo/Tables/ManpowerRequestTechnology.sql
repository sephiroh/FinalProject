CREATE TABLE [dbo].[ManpowerRequestTechnology] (
    [ManpowerRequestID] INT NOT NULL,
    [TechnologyID]      INT NOT NULL,
    CONSTRAINT [PK_ManpowerRequestTechnology] PRIMARY KEY NONCLUSTERED ([ManpowerRequestID] ASC, [TechnologyID] ASC),
    CONSTRAINT [FK_ManpowerRequestTechnology_ManpowerRequest] FOREIGN KEY ([ManpowerRequestID]) REFERENCES [dbo].[ManpowerRequest] ([Id]),
    CONSTRAINT [FK_ManpowerRequestTechnology_Technology] FOREIGN KEY ([TechnologyID]) REFERENCES [dbo].[Technology] ([Id])
);






GO


