namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBreakdownModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breakdowns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        RoomNumber = c.Short(),
                        Sender = c.String(),
                        Severity = c.Int(nullable: false),
                        Acknowledged = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Breakdowns", new[] { "IsDeleted" });
            DropTable("dbo.Breakdowns");
        }
    }
}
