using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class RacasController : BaseController
    {
        // GET: Raca
        public ActionResult Index()
        {
            if (!verifica_acesso("Raças", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Raças";
            RacaDao _RacaDao = new RacaDao();
            IList<Raca> _Racas = _RacaDao.Listar_Racas_grid();
            return View(_Racas);
        }

        [Route("Raça/{id}", Name="Editar_Raca")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("Raças", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Raças / Detalhes";
            RacaDao _RacaDao = new RacaDao();
            Raca _Raca = _RacaDao.Listar_Raca(id);
            return View(_Raca);
        }
    }
}