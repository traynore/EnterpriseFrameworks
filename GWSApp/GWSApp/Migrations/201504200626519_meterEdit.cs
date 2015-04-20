namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meterEdit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MeterReadings", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MeterReadings", "Year", c => c.Int(nullable: false));
        }
    }
}
