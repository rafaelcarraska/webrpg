using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class VantagensController : BaseController
    {
        // GET: Vantagem
        public ActionResult Index()
        {
            if (!verifica_acesso("Vantagens", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Vantagens";
            return View();
        }
    }
}