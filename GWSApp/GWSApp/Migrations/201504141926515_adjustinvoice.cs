namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjustinvoice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "SubtotalB", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "SubtotalC", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "SubtotalD", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "SubtotalE", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "Total", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "Arrears", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "GrandTotal", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "AmountPaid", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "GrandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Arrears", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "SubtotalE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "SubtotalD", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "SubtotalC", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "SubtotalB", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
