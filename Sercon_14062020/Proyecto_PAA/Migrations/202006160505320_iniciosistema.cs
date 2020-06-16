namespace Proyecto_PAA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniciosistema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Establecimientoes",
                c => new
                    {
                        EstablecimientoId = c.Int(nullable: false, identity: true),
                        EstablecimientoRbd = c.String(nullable: false),
                        EstablecimientoNombre = c.String(nullable: false),
                        EstablecimientoDirecccion = c.String(nullable: false),
                        EstablecimientoFono = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EstablecimientoId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        EstablecimientoId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                        BirthDate = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Establecimientoes", t => t.EstablecimientoId, cascadeDelete: true)
                .Index(t => t.EstablecimientoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "EstablecimientoId", "dbo.Establecimientoes");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "EstablecimientoId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Establecimientoes");
        }
    }
}
