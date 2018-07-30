using System.Collections.Generic;
using Tienda.CORE.Models;

namespace Tienda.CORE.ViewModels
{
    public class AdminProductoViewModel
    {
       public Producto Producto { get; set; }
        public IEnumerable<CategProducto> CategoriasProductos { get; set; }
    }
}
