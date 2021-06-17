namespace StaffTrainee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thuthemdong20bangUserInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserInfoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserInfoes", new[] { "UserId" });
            AlterColumn("dbo.UserInfoes", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserInfoes", "UserId");
            AddForeignKey("dbo.UserInfoes", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserInfoes", new[] { "UserId" });
            AlterColumn("dbo.UserInfoes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserInfoes", "UserId");
            AddForeignKey("dbo.UserInfoes", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
