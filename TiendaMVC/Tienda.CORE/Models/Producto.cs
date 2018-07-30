using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public class Producto : EntidadBase
    {
        [StringLength(20)]
        [DisplayName("Nombre del Producto")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [Range(0, 1000)]
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }

    }
}
