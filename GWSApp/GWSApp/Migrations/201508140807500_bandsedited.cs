namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bandsedited : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invoices", "QtyRateE");
            DropColumn("dbo.Invoices", "SubtotalE");
            DropColumn("dbo.RatesObjects", "BandD");
            DropColumn("dbo.RatesObjects", "RateE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RatesObjects", "RateE", c => c.Single(nullable: false));
            AddColumn("dbo.RatesObjects", "BandD", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "SubtotalE", c => c.Single(nullable: false));
            AddColumn("dbo.Invoices", "QtyRateE", c => c.Int(nullable: false));
        }
    }
}
