namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeitemreserveproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "canReserve", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "isReserved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "isReserved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "canReserve");
        }
    }
}
