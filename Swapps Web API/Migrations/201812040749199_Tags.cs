namespace Swapps_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Establishments", "TagsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Establishments", "TagsString");
        }
    }
}
