using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Filtros;
using rpg.Models;

namespace rpg.Controllers
{
    public class HomeController : BaseController
    {
    
        public ActionResult Index()
        {
            ViewBag.pagina = "Inicio";
            return RedirectToAction("Home", "Home");
        }

        [Route("Home", Name = "Home")]
        public ActionResult Home()
        {
            ViewBag.pagina = "Inicio";
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.cbmestre = _CampanhaDao.Listar_Campanhas_cb_mestre();
            ViewBag.cbjogador = _CampanhaDao.Listar_Campanhas_cb();
            return View();
        }

        [Route("Mestre/{id}", Name = "Mestre")]
        public ActionResult Mestre(int id)
        {
            return View();
        }

        [Route("Personagem/{id}", Name = "Personagem")]
        public ActionResult Personagem(int id)
        {
            ViewBag.idjpg = id+".jpg";
            return View();
        }

        [Route("Ficha/{id}", Name = "Ficha")]
        public ActionResult Ficha(int id)
        {
            ViewBag.idjpg = id + ".jpg";
            ViewBag.hp = 20;
            return View();
        }

        [Route("Editar_Personagem/{id}", Name = "Editar_Personagem")]
        public ActionResult Editar_Personagem(int id)
        {
            ViewBag.pagina = "Editar_Personagem";
            ViewBag.id = id;
            return View();
        }
    }
}