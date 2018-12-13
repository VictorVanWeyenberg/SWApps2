namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StrangeSuddenChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Establishments", "User_ID", "dbo.Users");
            DropIndex("dbo.Establishments", new[] { "User_ID" });
            CreateTable(
                "dbo.UserSubscriptions",
                c => new
                    {
                        EstablishmentRefId = c.Int(nullable: false),
                        UserRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EstablishmentRefId, t.UserRefId })
                .ForeignKey("dbo.Users", t => t.EstablishmentRefId, cascadeDelete: true)
                .ForeignKey("dbo.Establishments", t => t.UserRefId, cascadeDelete: true)
                .Index(t => t.EstablishmentRefId)
                .Index(t => t.UserRefId);
            
            DropColumn("dbo.Establishments", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Establishments", "User_ID", c => c.Int());
            DropForeignKey("dbo.UserSubscriptions", "UserRefId", "dbo.Establishments");
            DropForeignKey("dbo.UserSubscriptions", "EstablishmentRefId", "dbo.Users");
            DropIndex("dbo.UserSubscriptions", new[] { "UserRefId" });
            DropIndex("dbo.UserSubscriptions", new[] { "EstablishmentRefId" });
            DropTable("dbo.UserSubscriptions");
            CreateIndex("dbo.Establishments", "User_ID");
            AddForeignKey("dbo.Establishments", "User_ID", "dbo.Users", "ID");
        }
    }
}
