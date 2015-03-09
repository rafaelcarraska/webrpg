using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class ItensController : BaseController
    {
        // GET: Item
        public ActionResult Index()
        {
            if (!verifica_acesso("Itens", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Itens";
            return View();
        }
    }
}