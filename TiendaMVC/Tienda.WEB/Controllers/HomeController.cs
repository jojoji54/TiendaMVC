using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;
using Tienda.CORE.ViewModels;

namespace Tienda.WEB.Controllers
{
    public class HomeController : Controller
    {
        IRepositorio<Producto> context;
        IRepositorio<CategProducto> CategoriasProductos;

        public HomeController(IRepositorio<Producto> productoContext, IRepositorio<CategProducto> productoCategoriaContext)
        {
            context = productoContext;
            CategoriasProductos = productoCategoriaContext;
        }

        public ActionResult Index(string Categoria=null)
        {
            List<Producto> productos;
            List<CategProducto> categorias = CategoriasProductos.Collection().ToList();

            if (Categoria == null)
            {
                productos = context.Collection().ToList();
            }
            else {
                productos = context.Collection().Where(p => p.Categoria == Categoria).ToList();
            }

            ListaProductosViewModel model = new ListaProductosViewModel();
            model.Productos = productos;
            model.CategoriaProducto = categorias;


            return View(model);
        }

        public ActionResult Detalles(string Id) {
            Producto producto = context.Buscar(Id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            else {
                return View(producto);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}