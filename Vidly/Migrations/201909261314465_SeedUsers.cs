namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'53901f95-114d-4a26-a364-f7399a8fa1ee', N'guest@vidly.com', 0, N'ACOQkHB/hgTWBcTg4I0/bDBHNvT+u6YBKWkT1/D5ZM5VFgbGz0MPgec81THOknHBCw==', N'7fb9c6ea-6168-4335-a475-60d784799095', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'683c2993-80b3-4c58-b552-6361ffe8dc08', N'admin@vidly.com', 0, N'AEslipmlL/QnPpFjJWhOvufG1rTb8dDHh45bTU6YqSgyCbBK8zrO+vbc7P496DOlYg==', N'6a219e39-1442-4370-81c2-fd73a1afc30f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f8cb7e7b-63c1-4a69-94a7-d0ba8a332342', N'CanManageMovies')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'683c2993-80b3-4c58-b552-6361ffe8dc08', N'f8cb7e7b-63c1-4a69-94a7-d0ba8a332342')
        ");
        }
        
        public override void Down()
        {
        }
    }
}
