namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201908251742188_ChangeItemReservationProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemReservations", "RecurrenceID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemReservations", "RecurrenceID", c => c.Int(nullable: false));
        }
    }
}
