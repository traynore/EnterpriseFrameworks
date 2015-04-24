namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicePaidFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "_Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "_Paid");
        }
    }
}
