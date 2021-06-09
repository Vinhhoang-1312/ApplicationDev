namespace StaffTrainee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniquecategories : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Categories", "Name", unique: true, name: "Name_Index");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", "Name_Index");
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
    }
}
