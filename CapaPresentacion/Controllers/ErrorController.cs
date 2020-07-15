using System;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(String msgError)
        {
            ViewBag.error = msgError;
            return View();
        }
    }
}