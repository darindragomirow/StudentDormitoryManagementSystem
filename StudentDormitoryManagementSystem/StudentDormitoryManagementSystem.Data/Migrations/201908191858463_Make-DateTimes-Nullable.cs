namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDateTimesNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "DateRegistered", c => c.DateTime());
            AlterColumn("dbo.Items", "ExpirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Items", "DateRegistered", c => c.DateTime(nullable: false));
        }
    }
}
