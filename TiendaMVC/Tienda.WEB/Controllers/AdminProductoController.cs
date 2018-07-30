using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Tienda.CORE.Models;
using Tienda.CORE.ViewModels;
using Tienda.CORE.Contracts;

namespace Tienda.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductoController : Controller
    {
        IRepositorio<Producto> context;
        IRepositorio<CategProducto> Categoriasproductos;

        public AdminProductoController(IRepositorio<Producto> productoContext, IRepositorio<CategProducto> productCategoriaContext) {
            context = productoContext;
            Categoriasproductos = productCategoriaContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Producto> productos = context.Collection().ToList();
            return View(productos);
        }

        public ActionResult Crear() {
            AdminProductoViewModel viewModel = new AdminProductoViewModel();

            viewModel.Producto = new Producto();
            viewModel.CategoriasProductos = Categoriasproductos.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Crear(Producto producto, HttpPostedFileBase archivo) {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }
            else {

                if (archivo != null) {
                    producto.Imagen = producto.Id + Path.GetExtension(archivo.FileName);
                    archivo.SaveAs(Server.MapPath("//Content//ProductImages//") + producto.Imagen);
                }

                context.Insertar(producto);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Editar(string Id) {
            Producto producto = context.Buscar(Id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            else {
                AdminProductoViewModel viewModel = new AdminProductoViewModel();
                viewModel.Producto = producto;
                viewModel.CategoriasProductos = Categoriasproductos.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Editar(Producto producto, string Id, HttpPostedFileBase archivo) {
            Producto ProductosHaEditar = context.Buscar(Id);

            if (ProductosHaEditar == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid) {
                    return View(producto);
                }

                if (archivo != null) {
                    ProductosHaEditar.Imagen = producto.Id + Path.GetExtension(archivo.FileName);
                    archivo.SaveAs(Server.MapPath("//Content//ProductImages//") + ProductosHaEditar.Imagen);
                }

                ProductosHaEditar.Categoria = producto.Categoria;
                ProductosHaEditar.Descripcion = producto.Descripcion;
                ProductosHaEditar.Nombre = producto.Nombre;
                ProductosHaEditar.Precio = producto.Precio;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(string Id)
        {
            Producto ProductosHaEliminar = context.Buscar(Id);

            if (ProductosHaEliminar == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductosHaEliminar);
            }
        }

        [HttpPost]
        [ActionName("Eliminar")]
        public ActionResult ConfirmarParaEliminar(string Id) {
            Producto productosHaEliminar = context.Buscar(Id);

            if (productosHaEliminar == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Eliminar(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}