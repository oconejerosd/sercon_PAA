namespace Proyecto_PAA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
