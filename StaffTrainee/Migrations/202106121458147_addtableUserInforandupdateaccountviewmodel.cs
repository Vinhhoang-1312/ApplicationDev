namespace StaffTrainee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addtableUserInforandupdateaccountviewmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Phone = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserId);

        }

        public override void Down()
        {
            DropTable("dbo.UserInfoes");
        }
    }
}
