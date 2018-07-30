using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public class OrdenEnvio : EntidadBase
    {
        public OrdenEnvio() {
            this.ItemsOrden = new List<ItemOrden>();
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string EstadoOrden { get; set; }
        public virtual ICollection<ItemOrden> ItemsOrden { get; set; }
    }
}
