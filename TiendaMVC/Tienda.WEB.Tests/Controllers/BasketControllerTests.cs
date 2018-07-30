using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tienda.WEB.Tests.Mocks;
using System.Linq;
using System.Web.Mvc;
using System.Security.Principal;
using Tienda.APLICATION;
using Tienda.CORE.Models;
using Tienda.CORE.Contracts;
using Tienda.WEB.Tests.Mocks;
using Tienda.CORE.ViewModels;
using Tienda.WEB.Controllers;

namespace Tienda.WEB.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //setup
            IRepositorio<Cesta> baskets = new MockContext<Cesta>();
            IRepositorio<Producto> products = new MockContext<Producto>();
            IRepositorio<OrdenEnvio> orders = new MockContext<OrdenEnvio>();
            IRepositorio<Cliente> customers = new MockContext<Cliente>();

            var httpContext = new MockHttpContext();


            ISCesta basketService = new SCesta(products, baskets);
            ISOrden orderService = new SOrden(orders);
            var controller = new CestaController(basketService, orderService, customers);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //basketService.AddToBasket(httpContext, "1");
            controller.AgregarACesta("1");

            Cesta basket = baskets.Collection().FirstOrDefault();


            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.ItemsCesta.Count);
            Assert.AreEqual("1", basket.ItemsCesta.ToList().FirstOrDefault().IdProducto);
        }

        [TestMethod]
        public void CanGetSummaryViewModel() {
            IRepositorio<Cesta> baskets = new MockContext<Cesta>();
            IRepositorio<Producto> products = new MockContext<Producto>();
            IRepositorio<OrdenEnvio> orders = new MockContext<OrdenEnvio>();
            IRepositorio<Cliente> customers = new MockContext<Cliente>();

            products.Insertar(new Producto() { Id = "1", Precio = 10.00m });
            products.Insertar(new Producto() { Id = "2", Precio = 5.00m });

            Cesta basket = new Cesta();
            basket.ItemsCesta.Add(new ItemCesta() { IdProducto = "1", Cantidad = 2 });
            basket.ItemsCesta.Add(new ItemCesta() { IdProducto = "2", Cantidad = 1 });
            baskets.Insertar(basket);

            ISCesta basketService = new SCesta(products, baskets);
            ISOrden orderService = new SOrden(orders);
            var controller = new CestaController(basketService, orderService, customers);

            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("PrimussCesta") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            var result = controller.ResumenCesta() as PartialViewResult;
            var basketSummary = (ResumenCestaViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.Recuento);
            Assert.AreEqual(25.00m, basketSummary.TotalCesta);


        }

        [TestMethod]
        public void CanCheckoutAndCreateOrder() {
            IRepositorio<Cliente> customers = new MockContext<Cliente>();
            IRepositorio<Producto> products = new MockContext<Producto>();
            products.Insertar(new Producto() { Id = "1", Precio = 10.00m });
            products.Insertar(new Producto() { Id = "2", Precio = 5.00m });

            IRepositorio<Cesta> baskets = new MockContext<Cesta>();
            Cesta basket = new Cesta();
            basket.ItemsCesta.Add(new ItemCesta() { IdProducto = "1", Cantidad = 2, IdCesta = basket.Id });
            basket.ItemsCesta.Add(new ItemCesta() { IdProducto = "1", Cantidad = 1, IdCesta = basket.Id });

            baskets.Insertar(basket);

            ISCesta basketService = new SCesta(products, baskets);

            IRepositorio<OrdenEnvio> orders = new MockContext<OrdenEnvio>();
            ISOrden orderService = new SOrden(orders);

            customers.Insertar(new Cliente() { Id = "1", CorreoCliente = "brett.hargreaves@gmail.com", CodigoPostalCliente = "90210" });

            IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("brett.hargreaves@gmail.com", "Forms"), null);


            var controller = new CestaController(basketService, orderService, customers);
            var httpContext = new MockHttpContext();
            httpContext.User = FakeUser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            });

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            OrdenEnvio order = new OrdenEnvio();
            controller.Comprar(order);

            //assert
            Assert.AreEqual(2, order.ItemsOrden.Count);
            Assert.AreEqual(0, basket.ItemsCesta.Count);

            OrdenEnvio orderInRep = orders.Buscar(order.Id);
            Assert.AreEqual(2, orderInRep.ItemsOrden.Count);

        }
    }
}
