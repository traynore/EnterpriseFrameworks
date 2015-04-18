namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            DropColumn("dbo.Customers", "FirstMidName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "FirstMidName", c => c.String());
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
