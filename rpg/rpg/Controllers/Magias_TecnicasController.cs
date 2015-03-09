using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;

namespace rpg.Controllers
{
    public class Magias_TecnicasController : BaseController
    {
        // GET: Magia_Tecnica
        public ActionResult Index()
        {
            if (!verifica_acesso("Magias / Técnicas", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Magias / Técnicas";
            return View();
        }
    }
}