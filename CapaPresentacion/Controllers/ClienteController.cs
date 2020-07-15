using CapaAplicacion.Servicio;
using CapaDominio.Entidades;
using System;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteServicio clienteServicio = new ClienteServicio();
        // GET: Cliente
        public ActionResult Insertar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool insertar = clienteServicio.insertarCliente(cliente);
                    if (insertar)
                    {
                        return RedirectToAction("VerificarAcceso", "Login");
                    }
                    else
                    {
                        return View(cliente);
                    }
                }
                return View();
            }
            catch (Exception err)
            {
                return RedirectToAction("Error", "Error", new { msg = err.Message });
            }
        }
    }
}