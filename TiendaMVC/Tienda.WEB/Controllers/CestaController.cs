using System.Linq;
using System.Web.Mvc;
using Tienda.CORE.Contracts;
using Tienda.CORE.Models;

namespace Tienda.WEB.Controllers
{
    public class CestaController : Controller
    {
        IRepositorio<Cliente> clientes;
        ISCesta SerCesta;
        ISOrden serEnvio;

        public CestaController(ISCesta SCesta, ISOrden SEnvio, IRepositorio<Cliente> Clientes) {
            this.SerCesta = SCesta;
            this.serEnvio = SEnvio;
            this.clientes = Clientes;
        }
        // GET: Basket2
        public ActionResult Index()
        {
            var model = SerCesta.GetItemCesta(this.HttpContext);
            return View(model);
        }

        public ActionResult AgregarACesta(string Id)
        {
            SerCesta.AgregarACesta(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoverDeLaCesta(string Id)
        {
            SerCesta.RemoverDeCesta(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult ResumenCesta() {
            var ResumenCesta = SerCesta.GetResumenCesta(this.HttpContext);

            return PartialView(ResumenCesta);
        }

        [Authorize]
        public ActionResult Comprar() {
            Cliente cliente = clientes.Collection().FirstOrDefault(c => c.CorreoCliente == User.Identity.Name);

            if (cliente != null)
            {

                OrdenEnvio orden = new OrdenEnvio()
                {
                    Correo = cliente.CorreoCliente,
                    Ciudad = cliente.CiudadCliente,
                    Provincia = cliente.ProvinciaCliente,
                    Calle = cliente.CalleCliente,
                    Nombre = cliente.NombreCliente,
                    Apellido = cliente.ApellidoCliente,
                    CodigoPostal = cliente.CodigoPostalCliente
                };

                return View(orden);
            }
            else {
                return RedirectToAction("Error");
            }
            
        }

        [HttpPost]
        [Authorize]
        public ActionResult Comprar(OrdenEnvio orden) {

            var ItemsCesta = SerCesta.GetItemCesta(this.HttpContext);
            orden.EstadoOrden = "Orden Creada";
            orden.Correo = User.Identity.Name;

            //proceso de pago

            orden.EstadoOrden = "Pago Procesado";
            serEnvio.CrearOrden(orden, ItemsCesta);
            SerCesta.LimpiarCesta(this.HttpContext);

            return RedirectToAction("Agradecimiento", new { OrderId = orden.Id });
        }

        public ActionResult Agradecimiento(string IdEnvio) {
            ViewBag.IdEnvio = IdEnvio;
            return View();
        }
    }
}