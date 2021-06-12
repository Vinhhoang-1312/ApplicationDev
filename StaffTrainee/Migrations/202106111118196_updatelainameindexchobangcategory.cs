namespace StaffTrainee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updatelainameindexchobangcategory : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Categories", name: "Name_Index", newName: "IX_Name");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_Name", newName: "Name_Index");
        }
    }
}
