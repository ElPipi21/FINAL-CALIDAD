using System.Web.Mvc;

namespace CapaPresentacion.Filtros
{
    public class SesionExtranetAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["cliente"] == null)
            {
                filterContext.Result = new RedirectResult("~/Producto/ListarProductos");
            }
            base.OnActionExecuted(filterContext);
        }
    }
}