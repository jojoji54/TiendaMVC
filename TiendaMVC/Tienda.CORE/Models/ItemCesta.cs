using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public class ItemCesta : EntidadBase
    {
        public string IdCesta { get; set; }
        public string IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
