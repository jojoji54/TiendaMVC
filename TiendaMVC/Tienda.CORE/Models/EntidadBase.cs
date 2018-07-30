using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public abstract class EntidadBase
    {
        public string Id { get; set; }
        public DateTimeOffset Fecha { get; set; } //Creado en

        public EntidadBase() {
            this.Id = Guid.NewGuid().ToString();
            this.Fecha = DateTime.Now;
        }
    }
}
