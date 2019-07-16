namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2728d7ab-467d-41c9-8946-ecb1bbc14dbe', N'admin@vidly.com', 0, N'AAUCWuhwPDESluzLJiqu+W/QL+9II3x6pXRXMAVRvXbq1wULgYesJI19ImX/KpnkYw==', N'dd1e2832-6cfc-4d3c-95be-8248fcb07ced', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'40803a36-e7ae-4aef-a329-cf8b0b63d226', N'guest@vidly.com', 0, N'AMUmqbenl2jtrLVQSQ15myeGZWcNG+JPzFPTaTxScsxnenf4N9lshEV4P4ISOefX9w==', N'da91d87b-dd92-4f5a-b89f-9410c5fc6b6c', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'df696c9b-fa26-4f46-b2b1-ad753ea50361', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2728d7ab-467d-41c9-8946-ecb1bbc14dbe', N'df696c9b-fa26-4f46-b2b1-ad753ea50361')
                ");
        }

        public override void Down()
        {
        }
    }
}
