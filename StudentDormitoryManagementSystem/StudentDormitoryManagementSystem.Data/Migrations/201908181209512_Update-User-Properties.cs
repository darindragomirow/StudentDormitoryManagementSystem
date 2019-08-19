namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.AspNetUsers", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetUsers", new[] { "UserRoleId" });
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.AspNetUsers", "DeletedOn");
            DropColumn("dbo.AspNetUsers", "CreatedOn");
            DropColumn("dbo.AspNetUsers", "ModifiedOn");
            DropColumn("dbo.AspNetUsers", "UserRoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserRoleId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserRoleId");
            CreateIndex("dbo.AspNetUsers", "IsDeleted");
            AddForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.UserRoles", "RoleId", cascadeDelete: true);
        }
    }
}
