namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceBands : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "QtyRateA", c => c.Int());
            AddColumn("dbo.Invoices", "QtyRateB", c => c.Int());
            AddColumn("dbo.Invoices", "QtyRateC", c => c.Int());
            AddColumn("dbo.Invoices", "QtyRateD", c => c.Int());
            AddColumn("dbo.Invoices", "QtyRateE", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "QtyRateE");
            DropColumn("dbo.Invoices", "QtyRateD");
            DropColumn("dbo.Invoices", "QtyRateC");
            DropColumn("dbo.Invoices", "QtyRateB");
            DropColumn("dbo.Invoices", "QtyRateA");
        }
    }
}
