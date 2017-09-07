CREATE TABLE [dbo].[TaggedApplicant]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicantId] INT NOT NULL, 
    [ReferenceNoId] INT NOT NULL, 
    [TagDate] DATETIME NOT NULL , 
    CONSTRAINT [FK_TaggedApplicant_Applicant] FOREIGN KEY ([ApplicantId]) REFERENCES [dbo].[Applicant]([Id]), 
    CONSTRAINT [FK_TaggedApplicant_Request] FOREIGN KEY ([ReferenceNoId]) REFERENCES [dbo].[ReferenceNumber]([Id])
)
