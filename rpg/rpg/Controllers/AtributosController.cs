using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;

namespace rpg.Controllers
{
    public class AtributosController : BaseController
    {
        // GET: Atributos
        public ActionResult Index()
        {
            if (!verifica_acesso("Atributos", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Atributos";
            //ViewBag.paginacao = "<a id=\"Primeira\" disabled=\"disabled\"><<</a>";
            AtributoDao _AtributosDAO = new AtributoDao();
            IList<Atributos> _Atributos = _AtributosDAO.Listar_Atributos_dt();
            return View(_Atributos);
        }

        [HttpPost]
        public ActionResult Adiciona(int cod_atributo, string descricao, bool ativo)
        {
            string msg = validar(descricao, cod_atributo);
            if (string.IsNullOrEmpty(msg))
            {
                AtributoDao _AtributoDao = new AtributoDao();
                if (cod_atributo == 0)                                              
                {
                    if (verifica_acesso("Atributos", "Novo"))
                    {
                        msg = _AtributoDao.Insert(descricao, ativo);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }
                }
                else
                {
                    if (verifica_acesso("Atributos", "Alterar"))
                    {
                        msg = _AtributoDao.update(cod_atributo, descricao, ativo);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }
                }                
            }
            return Json(msg);            
        }

        [HttpPost]
        public ActionResult delete(int cod_atributo)
        {
            if (verifica_acesso("Atributos", "Deletar"))
            {
                AtributoDao _AtributoDao = new AtributoDao();
                return Json(_AtributoDao.delete(cod_atributo));
            }
            return Json("Acesso não permitido");
        }

        public string validar (string descricao, int cod_atributo)
        {
            string msg = "";
            AtributoDao _AtributoDao = new AtributoDao();
            if (string.IsNullOrEmpty(descricao))
            {
                msg += " Campo descrição obrigatório /n";
            }
            if (descricao.Length > 150)
            {
                 msg += " Campo descrição não pode ter mais de 150 caracteres. /n";
            }
            if (!_AtributoDao.verifica_descricao(cod_atributo, descricao))
            {
                msg += " Já existe uma descrição com esse nome /n";
            }
            return msg;
        }
    }
}