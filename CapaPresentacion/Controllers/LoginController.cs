using CapaAplicacion.Servicio;
using CapaDominio.Entidades;
using System;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class LoginController : Controller
    {
        private ClienteServicio clienteServicio = new ClienteServicio();

        public ActionResult Logout()
        {
            Session["cliente"] = null;
            return RedirectToAction("VerificarAcceso", "Login");
        }
        // GET: Login
        [HttpGet]
        public ActionResult VerificarAcceso(String msg)
        {
            ViewBag.mensaje = msg;
            return View();
        }
        [HttpPost]
        public ActionResult VerificarAcceso(FormCollection frm, String msg)
        {
            try
            {
                String cliente = frm["Usuario"].ToString();
                String clave = frm["Password"].ToString();
                string mensaje = "";
                Cliente clienteAcceso = clienteServicio.verificarAcceso(cliente, clave, out mensaje);
                if (mensaje.Equals(""))
                {
                    Session["cliente"] = clienteAcceso;
                    return RedirectToAction("ListarProductos", "Producto");
                }
                else
                {
                    return RedirectToAction("VerificarAcceso", new { msg = mensaje });
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Index", new { msg = ex.Message });
            }
        }
    }
}