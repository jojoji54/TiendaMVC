using System.Collections.Generic;
using System.Web;
using Tienda.CORE.ViewModels;

namespace Tienda.CORE.Contracts
{
    public interface ISCesta
    {
        void AgregarACesta(HttpContextBase httpContext, string productoId);
        void RemoverDeCesta(HttpContextBase httpContext, string itemId);
        List<ItemCestaViewModel> GetItemCesta(HttpContextBase httpContext);
        ResumenCestaViewModel GetResumenCesta(HttpContextBase httpContext);
        void LimpiarCesta(HttpContextBase httpContext);

    }
}
