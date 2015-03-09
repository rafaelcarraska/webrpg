using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class ClassesController : BaseController
    {
        // GET: Classe
        public ActionResult Index()
        {
            if (!verifica_acesso("Classes", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Classes";
            ClasseDao _ClasseDao = new ClasseDao();
            IList<Classe> _Classes = _ClasseDao.Listar_Classes_dt();
            return View(_Classes);
        }

        [Route("Classe/{id}", Name = "Editar_Classe")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("Classes", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Classes / Detalhes";
            ClasseDao _ClasseDao = new ClasseDao();
            Classe _Classe = _ClasseDao.Listar_Classe(id);
            return View(_Classe);
        }
    }
}