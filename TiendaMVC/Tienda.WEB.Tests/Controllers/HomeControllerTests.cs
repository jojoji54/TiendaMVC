using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;
using Tienda.CORE.Models;
using Tienda.CORE.Contracts;
using Tienda.CORE.ViewModels;
using Tienda.WEB.Controllers;

namespace Tienda.WEB.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepositorio<Producto> productContext = new Mocks.MockContext<Producto>();
            IRepositorio<CategProducto> productCatgeoryContext = new Mocks.MockContext<CategProducto>();

            productContext.Insertar(new Producto());

            HomeController controller = new HomeController(productContext, productCatgeoryContext);

            var result = controller.Index() as ViewResult;
            var viewModel = (ListaProductosViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Productos.Count());

        }
    }
}
