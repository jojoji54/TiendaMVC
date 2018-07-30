using System.Collections.Generic;
using Tienda.CORE.Models;
using Tienda.CORE.ViewModels;

namespace Tienda.CORE.Contracts
{
    public interface ISOrden
    {
        void CrearOrden(OrdenEnvio OrdenBase, List<ItemCestaViewModel> ItemsCesta);
        List<OrdenEnvio> GetOrdenLista();
        OrdenEnvio GetOrden(string Id);
        void ActualizarOrden(OrdenEnvio updatedOrder);
    }
}
