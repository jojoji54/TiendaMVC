using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public class Cesta : EntidadBase
    {
        public virtual ICollection<ItemCesta> ItemsCesta { get; set; }

        public Cesta() {
            this.ItemsCesta = new List<ItemCesta>();
        }
    }
}
