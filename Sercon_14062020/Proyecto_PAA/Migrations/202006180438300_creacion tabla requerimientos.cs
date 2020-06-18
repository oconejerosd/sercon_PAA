namespace Proyecto_PAA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creaciontablarequerimientos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requerimientoes",
                c => new
                    {
                        RequerimientoId = c.Int(nullable: false, identity: true),
                        RequerimientoTipo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RequerimientoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Requerimientoes");
        }
    }
}
