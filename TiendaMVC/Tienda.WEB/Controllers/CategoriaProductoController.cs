using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;

namespace Tienda.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriaProductoController : Controller
    {
        IRepositorio<CategProducto> context;

        public CategoriaProductoController(IRepositorio<CategProducto> context)
        {
            this.context = context;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<CategProducto> categoriaProducto = context.Collection().ToList();
            return View(categoriaProducto);
        }

        public ActionResult Crear()
        {
            CategProducto Categoriaproducto = new CategProducto();
            return View(Categoriaproducto);
        }

        [HttpPost]
        public ActionResult Crear(CategProducto CategoriaProducto)
        {
            if (!ModelState.IsValid)
            {
                return View(CategoriaProducto);
            }
            else
            {
                context.Insertar(CategoriaProducto);
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Editar(string Id)
        {
            CategProducto categoriaProducto = context.Buscar(Id);
            if (categoriaProducto == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoriaProducto);
            }
        }

        [HttpPost]
        public ActionResult Editar(CategProducto producto, string Id)
        {
            CategProducto categoriaProductoHaEditar = context.Buscar(Id);

            if (categoriaProductoHaEditar == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(producto);
                }

                categoriaProductoHaEditar.Categoria = producto.Categoria;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(string Id)
        {
            CategProducto categoriaProductoHaEliminar = context.Buscar(Id);

            if (categoriaProductoHaEliminar == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoriaProductoHaEliminar);
            }
        }

        [HttpPost]
        [ActionName("Eliminar")]
        public ActionResult ConfirmDelete(string Id)
        {
            CategProducto categoriaProductoHaEliminar = context.Buscar(Id);

            if (categoriaProductoHaEliminar == null)
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