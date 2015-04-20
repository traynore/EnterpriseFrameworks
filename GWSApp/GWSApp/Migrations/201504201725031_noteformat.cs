namespace GWSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noteformat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "NoteText", c => c.String(nullable: false, maxLength: 160));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "NoteText", c => c.String());
        }
    }
}
