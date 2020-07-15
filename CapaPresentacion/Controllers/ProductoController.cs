using CapaAplicacion.Servicio;
using CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoServicio productoServicio = new ProductoServicio();

        // GET: Producto
        //[Filtros.SesionExtranet]
        public ActionResult ListarProductos(String msgError)
        {
            ViewBag.error = msgError;
            List<Producto> listarProductos = productoServicio.listarProductos();
            return View(listarProductos);
        }
    }
}