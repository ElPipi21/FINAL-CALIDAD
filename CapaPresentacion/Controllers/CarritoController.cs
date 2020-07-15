using CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        [Filtros.SesionExtranet]
        public ActionResult ListarCarrito()
        {
            return View();
        }
        [Filtros.SesionExtranet]
        public ActionResult AgregarProductosACarrito(FormCollection formCollection)
        {
            try
            {
                List<LineaDePedido> lista;
                if (Session["carrito"] != null)
                {
                    lista = (List<LineaDePedido>)Session["carrito"];
                }
                else
                {
                    lista = new List<LineaDePedido>();
                }
                LineaDePedido lineaDePedido = new LineaDePedido();
                Producto producto = new Producto
                {
                    IdProducto = Convert.ToInt32(formCollection["IdProducto"]),
                    Nombre = formCollection["Nombre"].ToString(),
                    Descripcion = formCollection["Descripcion"].ToString(),
                    Imagen = formCollection["Imagen"].ToString()
                };

                lineaDePedido.CantidadVendida = Convert.ToInt32(formCollection["Cantidad"]);
                lineaDePedido.PrecioDeVenta = Convert.ToDouble(formCollection["Precio"]);
                lineaDePedido.Producto = producto;
                lista.Add(lineaDePedido);

                if (lineaDePedido.esCantidadValida())
                {
                    Session["carrito"] = lista;
                }
                else
                {
                    return RedirectToAction("ListarProductos", "Producto", new { msgError = "Excedió el máximo permitido que es de 20 unidades" });
                }
                return RedirectToAction("ListarCarrito");

            }
            catch (Exception error)
            {
                return RedirectToAction("Index", "Error", new { msgError = error.Message });

            }
        }
    }
}