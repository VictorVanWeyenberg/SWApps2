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
                        AddressID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Street = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Establishments",
                c => new
                    {
                        EstablishmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressID = c.Int(nullable: false),
                        EstablishmentTypeString = c.String(),
                    })
                .PrimaryKey(t => t.EstablishmentID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.EstablishmentEvents",
                c => new
                    {
                        EstablishmentEventID = c.Int(nullable: false, identity: true),
                        EstablishmentID = c.Int(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EstablishmentEventID)
                .ForeignKey("dbo.Establishments", t => t.EstablishmentID)
                .Index(t => t.EstablishmentID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionID = c.Int(nullable: false, identity: true),
                        EstablishmentID = c.Int(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PromotionID)
                .ForeignKey("dbo.Establishments", t => t.EstablishmentID)
                .Index(t => t.EstablishmentID);
            
            CreateTable(
                "dbo.TimeIntervals",
                c => new
                    {
                        TimeIntervalID = c.Int(nullable: false, identity: true),
                        EstablishmentID = c.Int(),
                        DayOfWeek = c.Int(nullable: false),
                        StartHour = c.Int(nullable: false),
                        StartMinute = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                        EndMinute = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimeIntervalID)
                .ForeignKey("dbo.Establishments", t => t.EstablishmentID)
                .Index(t => t.EstablishmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeIntervals", "EstablishmentID", "dbo.Establishments");
            DropForeignKey("dbo.Promotions", "EstablishmentID", "dbo.Establishments");
            DropForeignKey("dbo.EstablishmentEvents", "EstablishmentID", "dbo.Establishments");
            DropForeignKey("dbo.Establishments", "AddressID", "dbo.Addresses");
            DropIndex("dbo.TimeIntervals", new[] { "EstablishmentID" });
            DropIndex("dbo.Promotions", new[] { "EstablishmentID" });
            DropIndex("dbo.EstablishmentEvents", new[] { "EstablishmentID" });
            DropIndex("dbo.Establishments", new[] { "AddressID" });
            DropTable("dbo.TimeIntervals");
            DropTable("dbo.Promotions");
            DropTable("dbo.EstablishmentEvents");
            DropTable("dbo.Establishments");
            DropTable("dbo.Addresses");
        }
    }
}
