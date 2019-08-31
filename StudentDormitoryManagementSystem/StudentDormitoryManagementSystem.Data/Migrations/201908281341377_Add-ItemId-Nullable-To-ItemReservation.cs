namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemIdNullableToItemReservation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ItemReservations", name: "Item_Id", newName: "ItemId");
            RenameIndex(table: "dbo.ItemReservations", name: "IX_Item_Id", newName: "IX_ItemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ItemReservations", name: "IX_ItemId", newName: "IX_Item_Id");
            RenameColumn(table: "dbo.ItemReservations", name: "ItemId", newName: "Item_Id");
        }
    }
}
