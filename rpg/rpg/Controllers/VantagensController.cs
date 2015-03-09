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
            VantagemDao _VantagemDao = new VantagemDao();
            IList<Vantagem> _Vantagens = _VantagemDao.Listar_Vantagens_dt();
            return View(_Vantagens);
        }

        [Route("Vantagem/{id}", Name = "Editar_Vantagem")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("Vantagens", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Vantagem / Detalhes";
            VantagemDao _VantagemDao = new VantagemDao();
            Vantagem _Vantagens = _VantagemDao.Listar_Vantagem(id);
            AtributoDao _AtributoDao = new AtributoDao();
            ViewBag.Atributos = _AtributoDao.Listar_Atributos_ativo();
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            ViewBag.Vantagens = _VantagemDao.Listar_Vantagens_dt_cb();
            return View(_Vantagens);
        }

        [HttpPost]
        public ActionResult Adiciona(int cod_vantagem, string descricao, int custo, string atributos, string prerequisitovant, string prerequisito, String caracteristicas, int campanha, bool ativo)
        {
            string msg = "oi";
            Vantagem _vantagem = new Vantagem();
            _vantagem.Cod_Vantagem = cod_vantagem;
            _vantagem.Descricao = descricao;
            _vantagem.Custo = custo;
            _vantagem.Bonus_Atributo = new List<string>(limpar_list(atributos).Split(';'));
            
            return Json(msg);
        }

        public string limpar_list(string text)
        {
            if (text.Substring(text.Length-1) == "_" || text.Substring(text.Length-1) == ";")
            {
                return text.Substring(0, text.Length - 1);
            }
            return text;
        }
    }
}