namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addfunctionalityforitemreservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemReservations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReservedFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReservedTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Creator_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Creator_Id);
            
            AddColumn("dbo.Items", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "isReserved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Reservation_Id", c => c.Guid());
            CreateIndex("dbo.Items", "Reservation_Id");
            AddForeignKey("dbo.Items", "Reservation_Id", "dbo.ItemReservations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Reservation_Id", "dbo.ItemReservations");
            DropForeignKey("dbo.ItemReservations", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ItemReservations", new[] { "Creator_Id" });
            DropIndex("dbo.ItemReservations", new[] { "IsDeleted" });
            DropIndex("dbo.Items", new[] { "Reservation_Id" });
            DropColumn("dbo.Items", "Reservation_Id");
            DropColumn("dbo.Items", "isReserved");
            DropColumn("dbo.Items", "Type");
            DropTable("dbo.ItemReservations");
        }
    }
}
