namespace StaffTrainee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addmoilai : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "Name_Index");

            CreateTable(
                "dbo.Courses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CategoryId = c.Int(nullable: false),
                    Name = c.String(nullable: false),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.EnrollmentTrainees",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    CourseId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.EnrollmentTrainers",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    CourseId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.UserInfoes",
                c => new
                {
                    UserInfoId = c.Int(nullable: false, identity: true),
                    UserId = c.String(maxLength: 128),
                    FullName = c.String(nullable: false),
                    Phone = c.String(nullable: false),
                })
                .PrimaryKey(t => t.UserInfoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EnrollmentTrainers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EnrollmentTrainers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.EnrollmentTrainees", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EnrollmentTrainees", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.UserInfoes", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EnrollmentTrainers", new[] { "CourseId" });
            DropIndex("dbo.EnrollmentTrainers", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.EnrollmentTrainees", new[] { "CourseId" });
            DropIndex("dbo.EnrollmentTrainees", new[] { "UserId" });
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            DropIndex("dbo.Categories", "Name_Index");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EnrollmentTrainers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.EnrollmentTrainees");
            DropTable("dbo.Courses");
            DropTable("dbo.Categories");
        }
    }
}
