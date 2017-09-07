CREATE VIEW [dbo].[HrDashboardView]
AS
SELECT        refNo.Id, req.DateCreated AS 'RequestDate', refNo.ReferenceString AS 'RequestNumber', req.PrimarySkillID as 'PrimarySkillId', ps.Name AS 'PrimarySkillName', refNo.LevelId AS 'ApplicantLevelId', al.Name AS 'ApplicantLevelName', req.ProjectId as 'ProjectId', proj.Name AS 'ProjectName', { fn CONCAT(u.Firstname, 
                         { fn CONCAT(' ', u.Lastname) }) } AS 'ManagerName', u.Id AS 'ManagerId', { fn CONCAT(app.Firstname, { fn CONCAT(' ', app.Lastname) }) } AS 'ApplicantName', req.ProjectedStartDate AS 'ApplicantStartDate', refNo.StatusID AS 'Status', refNo.Reason AS 'Reason',
						 refNo.DateUpdated AS 'DateFilled'
FROM            dbo.ReferenceNumber AS refNo LEFT JOIN
				dbo.Applicant AS app ON refNo.ApplicantId = app.Id INNER JOIN
                dbo.ManpowerRequest AS req ON refNo.ManpowerRequestId = req.Id INNER JOIN
                dbo.ApplicantLevel AS al ON refNo.LevelId = al.Id INNER JOIN
                dbo.PrimarySkill AS ps ON req.PrimarySkillID = ps.Id INNER JOIN
                dbo.Project AS proj ON req.ProjectId = proj.Id INNER JOIN
                dbo.[User] AS u ON req.RequestedBy = u.Id INNER JOIN
                dbo.Status AS s ON refNo.StatusID = s.Id
GO
