/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [MagenicManpowerDB]
GO
SET IDENTITY_INSERT [dbo].[ApplicantLevel] ON 

INSERT [dbo].[ApplicantLevel] ([Id], [Name], [Description], [DateCreated], [DateUpdated], [IsActive]) VALUES (1, N'Junior', N'Beginner', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL, 1)
INSERT [dbo].[ApplicantLevel] ([Id], [Name], [Description], [DateCreated], [DateUpdated], [IsActive]) VALUES (2, N'Mid', NULL, CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL, 1)
INSERT [dbo].[ApplicantLevel] ([Id], [Name], [Description], [DateCreated], [DateUpdated], [IsActive]) VALUES (4, N'Senior', NULL, CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL, 1)
INSERT [dbo].[ApplicantLevel] ([Id], [Name], [Description], [DateCreated], [DateUpdated], [IsActive]) VALUES (5, N'Team Lead', N'A senior who can lead teams', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[ApplicantLevel] OFF
SET IDENTITY_INSERT [dbo].[MagenicRegion] ON 

INSERT [dbo].[MagenicRegion] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (1, N'Manila', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL)
INSERT [dbo].[MagenicRegion] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (2, N'Minneapolis', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL)
INSERT [dbo].[MagenicRegion] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (3, N'Boston', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[MagenicRegion] OFF
SET IDENTITY_INSERT [dbo].[PrimarySkill] ON 

INSERT [dbo].[PrimarySkill] ([Id], [Name], [Description], [DateCreated], [DateUpdated], [IsActive]) VALUES (1, N'.Net', N'Microsoft Development Tools', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[PrimarySkill] OFF
SET IDENTITY_INSERT [dbo].[Technology] ON 

INSERT [dbo].[Technology] ([Id], [Name], [Description], [DateCreated], [DateUpdated], [IsActive]) VALUES (1, N'AngularJS', N'Javascript framework', CAST(N'2017-01-18T07:11:17.537' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Technology] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'Open')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Filled')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Cancelled')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[Permission] ON 

INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (1, N'Application Management', N'User can perform administrative tasks')
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (2, N'Request Manpower', N'User can request for manpower')
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (3, N'Cancel Request', N'User can cancel manpower request')
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (4, N'Tag Applicants', N'User can tag applicants to reference numbers')
SET IDENTITY_INSERT [dbo].[Permission] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (1, N'administrator', CAST(N'2017-01-18T07:19:18.493' AS DateTime), NULL)
INSERT [dbo].[Role] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (2, N'hr', CAST(N'2017-01-18T07:19:18.493' AS DateTime), NULL)
INSERT [dbo].[Role] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (3, N'cm', CAST(N'2017-01-18T07:19:18.493' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 


INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (1, 1)
INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (1, 2)
INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (1, 3)
INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (1, 4)
INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (2, 4)
INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (3, 2)
INSERT [dbo].[RolePermission] ([RoleId], [PermissionId]) VALUES (3, 3)

DECLARE @salt binary(16)
SET @salt = NEWID()
INSERT [dbo].[User] ([Id], [Email], [Firstname], [Lastname], [PasswordHash], [Salt],[RoleId], [ContactNo]) VALUES (1, N'admin@magenic.com', N'Admin', N'Admin', HASHBYTES('SHA2_256', CAST(N'admadm12345' as binary(32)) + @salt), @salt, 1, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF


SET IDENTITY_INSERT [dbo].[ApplicantStatus] ON

INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (1, N'New')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (2, N'For Technical Exam')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (3, N'Passed Technical Exam')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (4, N'Failed Technical Exam')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (5, N'For HR Interview')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (6, N'Passed HR Interview')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (7, N'Failed HR Interview')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (8, N'For Comprehensive Interview')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (9, N'Passed Comprehensive Interview')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (10, N'Failed Comprehensive Interview')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (11, N'For Job Offer')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (12, N'Declined Offer')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (13, N'Accepted Offer')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (14, N'Withdrew Application')
INSERT [dbo].[ApplicantStatus] ([Id], [Name]) VALUES (15, N'Hired')

SET IDENTITY_INSERT [dbo].[ApplicantStatus] OFF
