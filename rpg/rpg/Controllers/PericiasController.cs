using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class PericiasController : BaseController
    {
        // GET: Pericia
        public ActionResult Index()
        {
            if (!verifica_acesso("Perícias", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Perícias";
            return View();
        }
    }
}