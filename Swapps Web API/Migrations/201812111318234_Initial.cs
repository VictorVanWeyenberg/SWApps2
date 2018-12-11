namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Street = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Establishments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(),
                        AddressID = c.Int(nullable: false),
                        EstablishmentTypeString = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.EstablishmentEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EstablishmentID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Establishments", t => t.EstablishmentID, cascadeDelete: true)
                .Index(t => t.EstablishmentID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EstablishmentID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Establishments", t => t.EstablishmentID, cascadeDelete: true)
                .Index(t => t.EstablishmentID);
            
            CreateTable(
                "dbo.TimeIntervals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        StartHour = c.Int(nullable: false),
                        StartMinute = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                        EndMinute = c.Int(nullable: false),
                        Establishment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Establishments", t => t.Establishment_ID)
                .Index(t => t.Establishment_ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false),
                        Establishment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Establishments", t => t.Establishment_ID)
                .Index(t => t.Establishment_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Establishment_ID", "dbo.Establishments");
            DropForeignKey("dbo.TimeIntervals", "Establishment_ID", "dbo.Establishments");
            DropForeignKey("dbo.Promotions", "EstablishmentID", "dbo.Establishments");
            DropForeignKey("dbo.EstablishmentEvents", "EstablishmentID", "dbo.Establishments");
            DropForeignKey("dbo.Establishments", "AddressID", "dbo.Addresses");
            DropIndex("dbo.Tags", new[] { "Establishment_ID" });
            DropIndex("dbo.TimeIntervals", new[] { "Establishment_ID" });
            DropIndex("dbo.Promotions", new[] { "EstablishmentID" });
            DropIndex("dbo.EstablishmentEvents", new[] { "EstablishmentID" });
            DropIndex("dbo.Establishments", new[] { "AddressID" });
            DropTable("dbo.Tags");
            DropTable("dbo.TimeIntervals");
            DropTable("dbo.Promotions");
            DropTable("dbo.EstablishmentEvents");
            DropTable("dbo.Establishments");
            DropTable("dbo.Addresses");
        }
    }
}
