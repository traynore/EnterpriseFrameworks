namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notedate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "_Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notes", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notes", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notes", "_Date");
        }
    }
}
