namespace Proyecto_PAA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creaciontablaprioridad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prioridads",
                c => new
                    {
                        PrioridadId = c.Int(nullable: false, identity: true),
                        PrioridadTipo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PrioridadId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Prioridads");
        }
    }
}
