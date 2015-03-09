using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class FichasController : BaseController
    {
        // GET: Ficha
        public ActionResult Index()
        {
            if (!verifica_acesso("Fichas", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Fichas";
            FichaDao _FichaDao = new FichaDao();
            AtributoDao _AtributoDao = new AtributoDao();
            IList<Ficha> _Fichas = _FichaDao.Listar_Fichas();
            ViewBag.Atributos = _AtributoDao.Listar_Atributos_ativo();
            return View(_Fichas);
        }
    }
}