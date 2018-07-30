using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;
using Tienda.CORE.ViewModels;

namespace Tienda.APLICATION
{
    public class SCesta : ISCesta
    {
        IRepositorio<Producto> productoContext;
        IRepositorio<Cesta> cestaContext;

        public const string NombreCesta = "PrimussBasket";

        public SCesta(IRepositorio<Producto> ProductoContext, IRepositorio<Cesta> CestaContext) {
            this.cestaContext = CestaContext;
            this.productoContext = ProductoContext;
        }

        private Cesta GetCesta(HttpContextBase httpContext, bool crearIfNull) {
            HttpCookie cookie = httpContext.Request.Cookies.Get(NombreCesta);

            Cesta cesta = new Cesta();

            if (cookie != null)
            {
                string IdCesta = cookie.Value ;
                if (!string.IsNullOrEmpty(IdCesta))
                {
                    cesta = cestaContext.Buscar(IdCesta);
                }
                else
                {
                    if (crearIfNull)
                    {
                        cesta = CrearNuevaCesta(httpContext);
                    }
                }
            }
            else
            {
                if (crearIfNull)
                {
                    cesta = CrearNuevaCesta(httpContext);
                }
            }

            return cesta;
           
        }

        private Cesta CrearNuevaCesta(HttpContextBase httpContext) {
            Cesta cesta = new Cesta();
            cestaContext.Insertar(cesta);
            cestaContext.Commit();

            HttpCookie cookie = new HttpCookie(NombreCesta);
            cookie.Value = cesta.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return cesta;
        }

        public void AgregarACesta(HttpContextBase httpContext, string productoId)
        {

            Cesta cesta = new Cesta();
            cesta = GetCesta(httpContext, true);
            ItemCesta item = cesta.ItemsCesta.FirstOrDefault(i => i.IdProducto == productoId);

            if (item == null)
            {
                item = new ItemCesta()
                {
                    IdCesta = cesta.Id,
                    IdProducto = productoId,
                    Cantidad = 1


                };

                cesta.ItemsCesta.Add(item);
            }
            else {
                

                //item.BasketId = bas;
                //item.ProductId = prroductId;
                item.Cantidad = item.Cantidad + 1;
            }



            cestaContext.Commit();
        }

        public void RemoverDeCesta(HttpContextBase httpContext, string itemId) {
            Cesta cesta = GetCesta(httpContext, true);
            ItemCesta item = cesta.ItemsCesta.FirstOrDefault(i => i.Id == itemId);

            if (item != null) {
                cesta.ItemsCesta.Remove(item);
                cestaContext.Commit();
            }
        }

        public List<ItemCestaViewModel> GetItemCesta(HttpContextBase httpContext) {
            Cesta cesta = GetCesta(httpContext, false);

            if (cesta != null)
            {
                var resultados = (from b in cesta.ItemsCesta
                                  join p in productoContext.Collection() on b.IdProducto equals p.Id
                               select new ItemCestaViewModel()
                               {
                                   Id = b.Id,
                                   Cantidad = b.Cantidad,
                                   NombreProducto = p.Nombre,
                                   Imagen = p.Imagen,
                                   Precio = p.Precio
                               }
                              ).ToList();

                return resultados;
            }
            else {
                return new List<ItemCestaViewModel>();
            }
        }

        public ResumenCestaViewModel GetResumenCesta(HttpContextBase httpContext) {
            Cesta cesta = GetCesta(httpContext, false);
            ResumenCestaViewModel model = new ResumenCestaViewModel(0, 0);
            if (cesta != null)
            {
                int? recuento = (from item in cesta.ItemsCesta
                                 select item.Cantidad).Sum();

                decimal? totalCcesta = (from item in cesta.ItemsCesta
                                        join p in productoContext.Collection() on item.IdProducto equals p.Id
                                        select item.Cantidad * p.Precio).Sum();

                model.Recuento = recuento ?? 0;
                model.TotalCesta = totalCcesta ?? decimal.Zero;

                return model;
            }
            else {
                return model;
            }
        }

        public void LimpiarCesta(HttpContextBase httpContext) {
            Cesta cesta = GetCesta(httpContext, false);
            cesta.ItemsCesta.Clear();
            cestaContext.Commit();
        }
    }
}
