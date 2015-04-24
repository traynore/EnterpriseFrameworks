namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.Int(nullable: false),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Address4 = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        SubtotalB = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubtotalE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Arrears = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.MeterReadings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        NoteText = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.MeterReadings", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Notes", new[] { "CustomerID" });
            DropIndex("dbo.MeterReadings", new[] { "CustomerID" });
            DropIndex("dbo.Invoices", new[] { "CustomerID" });
            DropTable("dbo.Notes");
            DropTable("dbo.MeterReadings");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
        }
    }
}
