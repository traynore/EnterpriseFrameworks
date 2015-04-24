namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceBands2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "QtyRateA", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "QtyRateB", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "QtyRateC", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "QtyRateD", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "QtyRateE", c => c.Int(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateA", c => c.Single(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateB", c => c.Single(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateC", c => c.Single(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateD", c => c.Single(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateE", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RatesObjects", "RateE", c => c.Double(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateD", c => c.Double(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateC", c => c.Double(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateB", c => c.Double(nullable: false));
            AlterColumn("dbo.RatesObjects", "RateA", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "QtyRateE", c => c.Int());
            AlterColumn("dbo.Invoices", "QtyRateD", c => c.Int());
            AlterColumn("dbo.Invoices", "QtyRateC", c => c.Int());
            AlterColumn("dbo.Invoices", "QtyRateB", c => c.Int());
            AlterColumn("dbo.Invoices", "QtyRateA", c => c.Int());
        }
    }
}
