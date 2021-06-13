namespace StaffTrainee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnumberforPhoneatUserInfortable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "Phone", c => c.Int(nullable: false));
        }
    }
}
