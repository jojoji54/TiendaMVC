using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.ViewModels
{
    public class ResumenCestaViewModel
    {
        public int Recuento { get; set; }
        public decimal TotalCesta { get; set; }

        public ResumenCestaViewModel() {
        }

        public ResumenCestaViewModel(int recuento, int totalCesta) {
            this.Recuento = totalCesta;
            this.TotalCesta = recuento;
        }
    }
}
