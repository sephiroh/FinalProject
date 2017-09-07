CREATE TABLE [dbo].[ManpowerRequest] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ProjectId]             INT NOT NULL,
    [PrimarySkillID]          INT            NOT NULL,
    [RegionID]                INT            NOT NULL,
    [ProjectedStartDate]      DATETIME       NOT NULL,
    [JobDescription]          NVARCHAR (MAX) NOT NULL,
    [RequestedBy]             INT            NOT NULL,
    [DateCreated]             DATETIME       NOT NULL,
    [DateUpdated]             DATETIME       NULL,
    [IsForReplacement] BIT NOT NULL DEFAULT 0, 
    [IsForAdditionalResource] BIT NOT NULL DEFAULT 0, 
    [IsChangeRequest] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_ManpowerRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManpowerRequest_MagenicRegion] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[MagenicRegion] ([Id]),
    CONSTRAINT [FK_ManpowerRequest_PrimarySkill] FOREIGN KEY ([PrimarySkillID]) REFERENCES [dbo].[PrimarySkill] ([Id]), 
    CONSTRAINT [FK_ManpowerRequest_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project]([Id])
); 


