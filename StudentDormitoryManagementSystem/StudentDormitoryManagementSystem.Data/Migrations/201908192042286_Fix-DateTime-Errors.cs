
namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateTimeErrors : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inventories", "LastDateModified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Inventories", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Inventories", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Inventories", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Items", "DateRegistered", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Items", "ExpirationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Items", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Items", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Items", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ItemCategories", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ItemCategories", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ItemCategories", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rooms", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rooms", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rooms", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.StudentInfoes", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.StudentInfoes", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.StudentInfoes", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.StudentInfoes", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.StudentInfoes", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.StudentInfoes", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Rooms", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Rooms", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Rooms", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.ItemCategories", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.ItemCategories", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.ItemCategories", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Items", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Items", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Items", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Items", "ExpirationDate", c => c.DateTime());
            AlterColumn("dbo.Items", "DateRegistered", c => c.DateTime());
            AlterColumn("dbo.Inventories", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Inventories", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Inventories", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Inventories", "LastDateModified", c => c.DateTime(nullable: false));
        }
    }
}
