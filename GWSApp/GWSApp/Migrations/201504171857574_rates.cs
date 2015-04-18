namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatesObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BandA = c.Int(nullable: false),
                        BandB = c.Int(nullable: false),
                        BandC = c.Int(nullable: false),
                        BandD = c.Int(nullable: false),
                        RateA = c.Double(nullable: false),
                        RateB = c.Double(nullable: false),
                        RateC = c.Double(nullable: false),
                        RateD = c.Double(nullable: false),
                        RateE = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatesObjects");
        }
    }
}
