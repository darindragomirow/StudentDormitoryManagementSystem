namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentInfo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Students", newName: "StudentInfoes");
            DropForeignKey("dbo.Inventories", "Id", "dbo.Students");
            DropIndex("dbo.Inventories", new[] { "Id" });
            AddColumn("dbo.AspNetUsers", "StudentInfoId", c => c.Guid());
            AlterColumn("dbo.StudentInfoes", "InventoryId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StudentInfoes", "InventoryId");
            CreateIndex("dbo.AspNetUsers", "StudentInfoId");
            AddForeignKey("dbo.AspNetUsers", "StudentInfoId", "dbo.StudentInfoes", "Id");
            AddForeignKey("dbo.StudentInfoes", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
            DropColumn("dbo.Inventories", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "StudentId", c => c.String());
            DropForeignKey("dbo.StudentInfoes", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.AspNetUsers", "StudentInfoId", "dbo.StudentInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "StudentInfoId" });
            DropIndex("dbo.StudentInfoes", new[] { "InventoryId" });
            AlterColumn("dbo.StudentInfoes", "InventoryId", c => c.Guid());
            DropColumn("dbo.AspNetUsers", "StudentInfoId");
            CreateIndex("dbo.Inventories", "Id");
            AddForeignKey("dbo.Inventories", "Id", "dbo.Students", "Id");
            RenameTable(name: "dbo.StudentInfoes", newName: "Students");
        }
    }
}
