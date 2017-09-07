CREATE VIEW [dbo].[TaggedApplicantView]
AS 
SELECT t.Id, t.ApplicantId, a.Firstname, a.Lastname, a.Email, a.Status, t.ReferenceNoId, t.TagDate, ref.LevelId, ref.ReferenceString, p.Name as 'ProjectName'
FROM [dbo].[TaggedApplicant] t 
join [dbo].[Applicant] a on a.Id = t.ApplicantId
join [dbo].[ReferenceNumber] ref on ref.Id = t.ReferenceNoId
join [dbo].[ManpowerRequest] mr on mr.Id = ref.ManpowerRequestId
join [dbo].[Project] p on p.Id = mr.ProjectId
GO