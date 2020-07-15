using System.Web.Mvc;

namespace CapaPresentacion.Filtros
{
    public class SessionIntranetAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/VerificarAcceso?msg=Debeiniciarsesion");
            }
            base.OnActionExecuted(filterContext);
        }
    }
}