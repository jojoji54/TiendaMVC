using System.Collections.Generic;
using System.Linq;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;
using Tienda.CORE.ViewModels;

namespace Tienda.APLICATION
{
    public class SOrden : ISOrden
    {
        IRepositorio<OrdenEnvio> ordenContext;
        public SOrden(IRepositorio<OrdenEnvio> OrdenContext) {
            this.ordenContext = OrdenContext;
        }

        public void CrearOrden(OrdenEnvio OrdenBase, List<ItemCestaViewModel> ItemsCesta)
        {
            foreach (var item in ItemsCesta) {
                OrdenBase.ItemsOrden.Add(new ItemOrden()
                {
                    IdProducto = item.Id,
                    Imagen = item.Imagen,
                    Precio = item.Precio,
                    NombreProducto = item.NombreProducto,
                    Cantidad = item.Cantidad
                });
            }

            ordenContext.Insertar(OrdenBase);
            ordenContext.Commit();
        }

        public List<OrdenEnvio> GetOrdenLista() {
            return ordenContext.Collection().ToList();
        }

        public OrdenEnvio GetOrden(string Id) {
            return ordenContext.Buscar(Id);
        }

        public void ActualizarOrden(OrdenEnvio actualizarOrden) {
            ordenContext.Actualizar(actualizarOrden);
            ordenContext.Commit();
        }
    }
}
