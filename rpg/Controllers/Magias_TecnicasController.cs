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
            Magia_TecnicaDao _Magia_TecnicaDao = new Magia_TecnicaDao();
            IList<Magia_Tecnica> _Magia_Tecnica = _Magia_TecnicaDao.Listar_Magia_Tecnicas_grid();
            return View(_Magia_Tecnica);
        }

        [Route("Magia_Tecnica/{id}", Name = "Editar_Magia_Tecnica")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("Magias / Técnicas", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Magias / Técnicas";

            RacaDao _RacaDao = new RacaDao();
            Raca _Raca = new Raca();
            
            if (id != 0)
            {
                _Raca = _RacaDao.Listar_Raca(id);
                if (_Raca.Cod_Raca == 0)
                {
                    return RedirectToAction("Index", "Itens");
                }      
            }
            else
            {
                _Raca.Ativo = true;
            }

            return View(_Raca);
        }
    }
}