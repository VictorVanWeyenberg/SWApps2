namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeIntervalNullability : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeIntervals", "StartHour", c => c.Int());
            AlterColumn("dbo.TimeIntervals", "StartMinute", c => c.Int());
            AlterColumn("dbo.TimeIntervals", "EndHour", c => c.Int());
            AlterColumn("dbo.TimeIntervals", "EndMinute", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeIntervals", "EndMinute", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeIntervals", "EndHour", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeIntervals", "StartMinute", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeIntervals", "StartHour", c => c.Int(nullable: false));
        }
    }
}
