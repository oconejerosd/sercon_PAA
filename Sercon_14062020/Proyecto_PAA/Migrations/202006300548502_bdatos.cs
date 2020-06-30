namespace Proyecto_PAA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bdatos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estadoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Activo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FechaGeneracion = c.DateTime(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        idEstado = c.Int(nullable: false),
                        idRequerimiento = c.Int(nullable: false),
                        PrioridadId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Solucion = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Estadoes", t => t.idEstado, cascadeDelete: true)
                .ForeignKey("dbo.Prioridads", t => t.PrioridadId, cascadeDelete: true)
                .ForeignKey("dbo.Requerimientoes", t => t.idRequerimiento, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.idEstado)
                .Index(t => t.idRequerimiento)
                .Index(t => t.PrioridadId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "idRequerimiento", "dbo.Requerimientoes");
            DropForeignKey("dbo.Tickets", "PrioridadId", "dbo.Prioridads");
            DropForeignKey("dbo.Tickets", "idEstado", "dbo.Estadoes");
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "PrioridadId" });
            DropIndex("dbo.Tickets", new[] { "idRequerimiento" });
            DropIndex("dbo.Tickets", new[] { "idEstado" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Estadoes");
        }
    }
}
