namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceBands1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "SubtotalA", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "SubtotalA");
        }
    }
}
