namespace Tienda.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategProductoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Categoria = c.String(),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cestas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemCestas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdCesta = c.String(),
                        IdProducto = c.String(),
                        Cantidad = c.Int(nullable: false),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                        Cesta_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cestas", t => t.Cesta_Id)
                .Index(t => t.Cesta_Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdCliente = c.String(),
                        NombreCliente = c.String(),
                        ApellidoCliente = c.String(),
                        CorreoCliente = c.String(),
                        CalleCliente = c.String(),
                        CiudadCliente = c.String(),
                        ProvinciaCliente = c.String(),
                        CodigoPostalCliente = c.String(),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrdenEnvios",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Correo = c.String(),
                        Calle = c.String(),
                        Ciudad = c.String(),
                        Provincia = c.String(),
                        CodigoPostal = c.String(),
                        EstadoOrden = c.String(),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemOrdens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdOrden = c.String(),
                        IdProducto = c.String(),
                        NombreProducto = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Imagen = c.String(),
                        Cantidad = c.Int(nullable: false),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                        OrdenEnvio_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrdenEnvios", t => t.OrdenEnvio_Id)
                .Index(t => t.OrdenEnvio_Id);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(maxLength: 20),
                        Descripcion = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Categoria = c.String(),
                        Imagen = c.String(),
                        Fecha = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemOrdens", "OrdenEnvio_Id", "dbo.OrdenEnvios");
            DropForeignKey("dbo.ItemCestas", "Cesta_Id", "dbo.Cestas");
            DropIndex("dbo.ItemOrdens", new[] { "OrdenEnvio_Id" });
            DropIndex("dbo.ItemCestas", new[] { "Cesta_Id" });
            DropTable("dbo.Productoes");
            DropTable("dbo.ItemOrdens");
            DropTable("dbo.OrdenEnvios");
            DropTable("dbo.Clientes");
            DropTable("dbo.ItemCestas");
            DropTable("dbo.Cestas");
            DropTable("dbo.CategProductoes");
        }
    }
}
