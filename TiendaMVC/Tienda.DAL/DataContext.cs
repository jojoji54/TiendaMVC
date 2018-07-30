using System.Data.Entity;
using Tienda.CORE.Models;

namespace Tienda.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection") {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<CategProducto> CategoriaProductos { get; set; }
        public DbSet<Cesta> Cesta { get; set; }
        public DbSet<ItemCesta> ItemsCesta { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<OrdenEnvio> Envios { get; set; }
        public DbSet<ItemOrden> ItemsOrden { get; set; }
    }
}
