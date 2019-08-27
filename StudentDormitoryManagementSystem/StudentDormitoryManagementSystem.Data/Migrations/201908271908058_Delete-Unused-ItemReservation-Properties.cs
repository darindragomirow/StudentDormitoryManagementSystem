namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUnusedItemReservationProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemReservations", "CategoryColor", c => c.String());
            AddColumn("dbo.ItemReservations", "Owner_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ItemReservations", "Owner_Id");
            AddForeignKey("dbo.ItemReservations", "Owner_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.ItemReservations", "StartTimezone");
            DropColumn("dbo.ItemReservations", "EndTimezone");
            DropColumn("dbo.ItemReservations", "RecurrenceRule");
            DropColumn("dbo.ItemReservations", "RecurrenceID");
            DropColumn("dbo.ItemReservations", "RecurrenceException");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemReservations", "RecurrenceException", c => c.String());
            AddColumn("dbo.ItemReservations", "RecurrenceID", c => c.Int(nullable: false));
            AddColumn("dbo.ItemReservations", "RecurrenceRule", c => c.String());
            AddColumn("dbo.ItemReservations", "EndTimezone", c => c.String());
            AddColumn("dbo.ItemReservations", "StartTimezone", c => c.String());
            DropForeignKey("dbo.ItemReservations", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ItemReservations", new[] { "Owner_Id" });
            DropColumn("dbo.ItemReservations", "Owner_Id");
            DropColumn("dbo.ItemReservations", "CategoryColor");
        }
    }
}
