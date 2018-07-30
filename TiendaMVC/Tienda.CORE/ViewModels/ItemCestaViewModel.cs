﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.CORE.ViewModels
{
    public class ItemCestaViewModel
    {
        public string Id { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
    }
}
