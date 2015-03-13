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
            PericiaDao _PericiaDao = new PericiaDao();
            IList<Pericia> _Pericia = _PericiaDao.Listar_Pericias_dt();
            return View(_Pericia);
        }


        [Route("Pericia/{id}", Name = "Editar_Pericia")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("pericias", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Pericia / Detalhes";
            PericiaDao _PericiaDao = new PericiaDao();
            Pericia _pericias = new Pericia();
            if (id != 0)
            {
                _pericias = _PericiaDao.Listar_Pericia(id);
            }
            AtributoDao _AtributoDao = new AtributoDao();
            ViewBag.Atributos = _AtributoDao.Listar_Atributos_ativo();
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            ClasseDao _ClasseDao = new ClasseDao();
            ViewBag.Classe = _ClasseDao.Listar_Classes_dt_cb();
            return View(_pericias);
        }

        [HttpPost]
        public ActionResult delete(int cod_pericia)
        {
            if (verifica_acesso("Pericias", "Deletar"))
            {
                PericiaDao _PericiaDao = new PericiaDao();
                return Json(_PericiaDao.delete(cod_pericia));
            }
            return Json("Acesso não permitido");
        }
    }
}