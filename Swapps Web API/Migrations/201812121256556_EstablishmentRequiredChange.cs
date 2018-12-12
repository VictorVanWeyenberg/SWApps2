namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstablishmentRequiredChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "AbstractUser_ID", newName: "AbstractUserID");
            RenameIndex(table: "dbo.Users", name: "IX_AbstractUser_ID", newName: "IX_AbstractUserID");
            AddColumn("dbo.AbstractUsers", "Salt", c => c.String(nullable: false));
            AddColumn("dbo.AbstractUsers", "Hash", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbstractUsers", "Hash");
            DropColumn("dbo.AbstractUsers", "Salt");
            RenameIndex(table: "dbo.Users", name: "IX_AbstractUserID", newName: "IX_AbstractUser_ID");
            RenameColumn(table: "dbo.Users", name: "AbstractUserID", newName: "AbstractUser_ID");
        }
    }
}
