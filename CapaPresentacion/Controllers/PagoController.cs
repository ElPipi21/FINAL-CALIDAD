using CapaAplicacion.Servicio;
using System;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class PagoController : Controller
    {
        private readonly RealizarPagoServicio realizarPagoServicio = new RealizarPagoServicio();
        // GET: Pago
        public ActionResult PagarPedido(int id)
        {
            try
            {
                bool paga = realizarPagoServicio.pagoPedido(id);
                return RedirectToAction("Pedido", "ListaPedidoPorCliente", new { mensaje = "¡Pago exitoso!" });

            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error", new { msgError = ex.Message });
            }

        }
    }
}