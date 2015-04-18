namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "InvoiceNumber", c => c.Int());
            AlterColumn("dbo.Customers", "JoinDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "JoinDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "InvoiceNumber", c => c.Int(nullable: false));
        }
    }
}
