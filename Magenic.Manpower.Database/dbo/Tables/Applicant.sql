CREATE TABLE [dbo].[Applicant]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Firstname] NVARCHAR(100) NOT NULL, 
    [Lastname] NVARCHAR(100) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [ContactNumber] NVARCHAR(20) NOT NULL, 
    [ResumeFile] VARBINARY(MAX) NULL, 
    [CurrentPosition] NVARCHAR(50) NULL, 
    [CurrentCompany] NVARCHAR(50) NULL, 
    [YearsITExperience] NVARCHAR(10) NULL, 
    [YearsForSpecificSkills] NVARCHAR(50) NULL, 
    [Status] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [DateUpdated] DATETIME NULL,
	[PrimarySkillId] INT NOT NULL, 
    [LevelId] INT NULL, 
    [NoticePeriod] NVARCHAR(50) NULL, 
    [PendingApplication] NVARCHAR(50) NULL, 
	[HireDate] DATETIME NULL, 
    CONSTRAINT [FK_Applicant_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[ApplicantStatus] ([Id]), 
    CONSTRAINT [FK_Applicant_PrimarySkill] FOREIGN KEY ([PrimarySkillId]) REFERENCES [dbo].[PrimarySkill]([Id]),
    CONSTRAINT [FK_Applicant_ApplicantLevel] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[ApplicantLevel]([Id])
)
