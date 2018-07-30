using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.Models
{
    public class Cliente : EntidadBase
    {
        public string IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string CalleCliente { get; set; }
        public string CiudadCliente { get; set; }
        public string ProvinciaCliente { get; set; }
        public string CodigoPostalCliente { get; set; }
    }
}
