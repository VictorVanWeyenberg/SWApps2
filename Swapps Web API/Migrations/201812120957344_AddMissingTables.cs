namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMissingTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbstractUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Entrepreneurs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        EstablishmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Establishments", t => t.EstablishmentID, cascadeDelete: true)
                .ForeignKey("dbo.AbstractUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.EstablishmentID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AbstractUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AbstractUsers", t => t.AbstractUser_ID, cascadeDelete: true)
                .Index(t => t.AbstractUser_ID);
            
            AddColumn("dbo.Establishments", "User_ID", c => c.Int());
            CreateIndex("dbo.Establishments", "User_ID");
            AddForeignKey("dbo.Establishments", "User_ID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Establishments", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "AbstractUser_ID", "dbo.AbstractUsers");
            DropForeignKey("dbo.Entrepreneurs", "UserID", "dbo.AbstractUsers");
            DropForeignKey("dbo.Entrepreneurs", "EstablishmentID", "dbo.Establishments");
            DropIndex("dbo.Users", new[] { "AbstractUser_ID" });
            DropIndex("dbo.Establishments", new[] { "User_ID" });
            DropIndex("dbo.Entrepreneurs", new[] { "EstablishmentID" });
            DropIndex("dbo.Entrepreneurs", new[] { "UserID" });
            DropColumn("dbo.Establishments", "User_ID");
            DropTable("dbo.Users");
            DropTable("dbo.Entrepreneurs");
            DropTable("dbo.AbstractUsers");
        }
    }
}
