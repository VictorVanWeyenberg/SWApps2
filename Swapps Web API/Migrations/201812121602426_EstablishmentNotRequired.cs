namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstablishmentNotRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entrepreneurs", "EstablishmentID", "dbo.Establishments");
            DropIndex("dbo.Entrepreneurs", new[] { "EstablishmentID" });
            AlterColumn("dbo.Entrepreneurs", "EstablishmentID", c => c.Int());
            CreateIndex("dbo.Entrepreneurs", "EstablishmentID");
            AddForeignKey("dbo.Entrepreneurs", "EstablishmentID", "dbo.Establishments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entrepreneurs", "EstablishmentID", "dbo.Establishments");
            DropIndex("dbo.Entrepreneurs", new[] { "EstablishmentID" });
            AlterColumn("dbo.Entrepreneurs", "EstablishmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Entrepreneurs", "EstablishmentID");
            AddForeignKey("dbo.Entrepreneurs", "EstablishmentID", "dbo.Establishments", "ID", cascadeDelete: true);
        }
    }
}
