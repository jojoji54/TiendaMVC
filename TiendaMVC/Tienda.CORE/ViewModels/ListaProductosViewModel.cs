using System.Collections.Generic;
using Tienda.CORE.Models;

namespace Tienda.CORE.ViewModels
{
    public class ListaProductosViewModel
    {
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<CategProducto> CategoriaProducto { get; set; }
    }
}
