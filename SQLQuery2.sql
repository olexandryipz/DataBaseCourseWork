USE PerfumeStore;
GO

INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES (NEWID(), N'Admin', N'ADMIN', NEWID());
GO

DECLARE @UserEmail NVARCHAR(256) = 'ipz231_soyu@student.ztu.edu.ua';

DECLARE @UserId NVARCHAR(450) = (SELECT Id FROM AspNetUsers WHERE NormalizedEmail = UPPER(@UserEmail));
DECLARE @RoleId NVARCHAR(450) = (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@UserId, @RoleId);
GO