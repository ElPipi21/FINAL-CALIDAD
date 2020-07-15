using CapaAplicacion.Servicio;
using CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class PedidoController : Controller
    {
        private readonly RealizarPedidoServicio realizarPedidoServicio = new RealizarPedidoServicio();
        private readonly RealizarPagoServicio realizarPagoServicio = new RealizarPagoServicio();
        GeoCoordinateWatcher geoCoordinateWatcher = new GeoCoordinateWatcher();
        // GET: Pedido
        [HttpGet]
        [Filtros.SessionIntranet]
        public ActionResult GuardarPedido()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GuardarPedido(FormCollection formCollection)
        {
            try
            {
                Pedido obtenerDatosPedido = new Pedido();
                Cliente obtenerDatosCliente = new Cliente();
                Ubicacion ubicacion = new Ubicacion();
                double longitud = 0, latitud = 0;
                geoCoordinateWatcher.PositionChanged += (S, E) =>
                {
                    var coordinate = E.Position.Location;
                    longitud = coordinate.Longitude;
                    latitud = coordinate.Latitude;
                };
                geoCoordinateWatcher.Start();

                //Obtenemos datos
                obtenerDatosPedido.Cliente = (Cliente)Session["cliente"];
                obtenerDatosPedido.ListaLineasDePedido = (List<LineaDePedido>)Session["carrito"];
                ubicacion.ObtenerDistancia(longitud, latitud, -8.115706, -79.044433);
                obtenerDatosPedido.Ubicacion = ubicacion;

                int idVenta = realizarPedidoServicio.insertarPedido(obtenerDatosPedido);
                if (idVenta > 0)
                {
                    Session["carrito"] = null;
                    return RedirectToAction("ListaPedidoPorCliente", "Pedido", new { mensaje = "¡Registro exitoso!" });
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { msgError = "Error al guardar pedido" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { msgError = ex.Message });
            }
        }
        public ActionResult ConfirmacionTransaccion(bool guardo, int idVenta)
        {
            ViewBag.Guardo = guardo;
            ViewBag.idVenta = idVenta;
            return View();
        }

        public ActionResult ListaPedidoPorCliente(string mensaje)
        {
            Cliente cliente = new Cliente();
            cliente = (Cliente)Session["cliente"];
            ViewBag.mensaje = mensaje;
            List<Pedido> pedidos = realizarPedidoServicio.listaPedidosPorCliente(cliente.IdCliente);
            return View(pedidos);
        }

        public ActionResult ListarDetallePedido(int id)
        {
            try
            {
                Pedido pedido = new Pedido();
                pedido = realizarPedidoServicio.listarDetallePedidoPorCliente(id);
                return View(pedido);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { msgError = ex.Message });
            }
        }
        // GET: Pago
        public ActionResult PagarPedido(int id)
        {
            try
            {
                bool paga = realizarPagoServicio.pagoPedido(id);
                return RedirectToAction("ListaPedidoPorCliente", "Pedido", new { mensaje = "¡Pago exitoso!" });

            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error", new { msgError = ex.Message });
            }

        }
    }
}