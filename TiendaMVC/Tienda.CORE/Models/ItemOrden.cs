using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public class ItemOrden : EntidadBase
    {
        public string IdOrden { get; set; }
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Cantidad { get; set; }
    }
}
