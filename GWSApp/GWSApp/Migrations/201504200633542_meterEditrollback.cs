namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meterEditrollback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeterReadings", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeterReadings", "Year");
        }
    }
}
