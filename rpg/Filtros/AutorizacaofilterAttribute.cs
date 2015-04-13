using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rpg.Filtros
{
    public class AutorizacaofilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object usuario = filterContext.HttpContext.Session["usuariologado"];
            if (usuario == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { Controller = "Login", Action = "Index" }
                        )
                    );
            }
        }
    }
}