namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeItemReservationProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemReservations", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "Reservation_Id", "dbo.ItemReservations");
            DropIndex("dbo.Items", new[] { "Reservation_Id" });
            DropIndex("dbo.ItemReservations", new[] { "Creator_Id" });
            AddColumn("dbo.Items", "ItemType", c => c.Int(nullable: false));
            AddColumn("dbo.ItemReservations", "StartTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ItemReservations", "EndTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ItemReservations", "Subject", c => c.String());
            AddColumn("dbo.ItemReservations", "IsAllDay", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemReservations", "StartTimezone", c => c.String());
            AddColumn("dbo.ItemReservations", "EndTimezone", c => c.String());
            AddColumn("dbo.ItemReservations", "RecurrenceRule", c => c.String());
            AddColumn("dbo.ItemReservations", "RecurrenceID", c => c.Int(nullable: false));
            AddColumn("dbo.ItemReservations", "RecurrenceException", c => c.String());
            AddColumn("dbo.ItemReservations", "Item_Id", c => c.Guid());
            CreateIndex("dbo.ItemReservations", "Item_Id");
            AddForeignKey("dbo.ItemReservations", "Item_Id", "dbo.Items", "Id");
            DropColumn("dbo.Items", "Type");
            DropColumn("dbo.Items", "Reservation_Id");
            DropColumn("dbo.ItemReservations", "ReservedFrom");
            DropColumn("dbo.ItemReservations", "ReservedTo");
            DropColumn("dbo.ItemReservations", "Creator_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemReservations", "Creator_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ItemReservations", "ReservedTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ItemReservations", "ReservedFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Items", "Reservation_Id", c => c.Guid());
            AddColumn("dbo.Items", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.ItemReservations", "Item_Id", "dbo.Items");
            DropIndex("dbo.ItemReservations", new[] { "Item_Id" });
            DropColumn("dbo.ItemReservations", "Item_Id");
            DropColumn("dbo.ItemReservations", "RecurrenceException");
            DropColumn("dbo.ItemReservations", "RecurrenceID");
            DropColumn("dbo.ItemReservations", "RecurrenceRule");
            DropColumn("dbo.ItemReservations", "EndTimezone");
            DropColumn("dbo.ItemReservations", "StartTimezone");
            DropColumn("dbo.ItemReservations", "IsAllDay");
            DropColumn("dbo.ItemReservations", "Subject");
            DropColumn("dbo.ItemReservations", "EndTime");
            DropColumn("dbo.ItemReservations", "StartTime");
            DropColumn("dbo.Items", "ItemType");
            CreateIndex("dbo.ItemReservations", "Creator_Id");
            CreateIndex("dbo.Items", "Reservation_Id");
            AddForeignKey("dbo.Items", "Reservation_Id", "dbo.ItemReservations", "Id");
            AddForeignKey("dbo.ItemReservations", "Creator_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
