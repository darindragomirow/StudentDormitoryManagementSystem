namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountToItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Count");
        }
    }
}
