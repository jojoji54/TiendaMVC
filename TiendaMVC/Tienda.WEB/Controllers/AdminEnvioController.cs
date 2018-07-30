using System.Collections.Generic;
using System.Web.Mvc;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;

namespace Tienda.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminEnvioController : Controller
    {
        ISOrden SOrden;

        public AdminEnvioController(ISOrden Sorden) {
            this.SOrden = Sorden;
        }
        // GET: OrderManager
        public ActionResult Index()
        {
            List<OrdenEnvio> ordenesEnvio = SOrden.GetOrdenLista();

            return View(ordenesEnvio);
        }

        public ActionResult ActualizarOrden(string Id) {
            ViewBag.StatusList = new List<string>() {
                "Orden Creada",
                "Pago Procesado",
                "Orden Enviada",
                "Orden Completada"
            };
            OrdenEnvio orden = SOrden.GetOrden(Id);
            return View(orden);
        }

        [HttpPost]
        public ActionResult ActualizarOrden(OrdenEnvio actualizarOrden, string Id) {
            OrdenEnvio orden = SOrden.GetOrden(Id);

            orden.EstadoOrden = actualizarOrden.EstadoOrden;
            SOrden.ActualizarOrden(orden);

            return RedirectToAction("Index");
        }
    }
}