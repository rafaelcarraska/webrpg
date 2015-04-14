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
        [Route("Vantagens", Name = "Vantagens")]
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
            Vantagem _Vantagens = new Vantagem();                    
            AtributoDao _AtributoDao = new AtributoDao();
            List<Atributos> listatributos = _AtributoDao.Listar_Atributos_ativo();
            ViewBag.Atributos = listatributos;
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            List<Vantagem> listvant = _VantagemDao.Listar_Vantagens_dt_cb();
            ViewBag.Vantagens = listvant;
            List<string> atributosload = new List<string>();
            List<string> vantagensload = new List<string>();
            if (id != 0)
            {
                _Vantagens = _VantagemDao.Listar_Vantagem(id);
                if (_Vantagens.Cod_Vantagem == 0)
                {
                    return RedirectToAction("Index", "Vantagens");
                }
                
                foreach (string item in _Vantagens.Bonus_Atributo)
                {
                    List<string> b_A = new List<string>(item.Split('_'));
                    foreach (Atributos atrib in listatributos)
                    {
                        if (atrib.Cod_Atributo.ToString() == b_A[0])
                        {
                            atributosload.Add(b_A[0] + "_" + b_A[1] + "_" + atrib.Descricao);
                            break;
                        }                        
                    }
                }       

                foreach (int item in _Vantagens.Pre_Vantagens)
                {
                    foreach (Vantagem van in listvant)
                    {
                        if (van.Cod_Vantagem == item)
                        {
                            vantagensload.Add(van.Cod_Vantagem + "_"+ van.Descricao);
                            break;
                        }                        
                    }
                }
                if ( _Vantagens.Pre_Requisitos.Count() > 0 )
                {
                    ViewBag.preatributo = _Vantagens.Pre_Requisitos.Split('_')[0].ToString();
                    if (_Vantagens.Pre_Requisitos.Split('_').Count() > 1)
                    {
                        ViewBag.preatributovalor = _Vantagens.Pre_Requisitos.Split('_')[1].ToString();
                    }
                    else
                    {
                        ViewBag.preatributovalor = "";
                    }
                    
                }
                else
                {
                    ViewBag.preatributo = "";
                    ViewBag.preatributovalor = "";
                }
                
            }
            ViewBag.atributosload = atributosload;
            ViewBag.vantagensload = vantagensload;
            return View(_Vantagens);
        }

        [HttpPost]
        public ActionResult Adiciona(int cod_vantagem, string descricao, int custo, string atributos, string prerequisitovant, string prerequisito, String caracteristicas, int campanha, bool ativo)
        {            
            Vantagem _vantagem = new Vantagem();
            _vantagem.Cod_Vantagem = cod_vantagem;
            _vantagem.Descricao = descricao;
            _vantagem.Custo = custo;
            _vantagem.Bonus_Atributo = new List<string>(limpar_list(atributos).Split(';'));
            if (string.IsNullOrEmpty(prerequisitovant))
            {
                prerequisitovant = "0";
            }
            _vantagem.Pre_Vantagens = new List<int>(Array.ConvertAll(limpar_list(prerequisitovant).Split('_'), int.Parse));
            _vantagem.Pre_Requisitos = prerequisito;
            _vantagem.Caracteristicas = caracteristicas;
            _vantagem.Campanha = campanha;
            _vantagem.Ativo = ativo;

            string msg = validar(_vantagem);
            if (string.IsNullOrEmpty(msg))
            {
                VantagemDao _VantagemDao = new VantagemDao();
                if (_vantagem.Cod_Vantagem == 0)
	            {
                    if (verifica_acesso("Vantagens", "Alterar"))
                    {
                        msg = _VantagemDao.Insert(_vantagem);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }                    
	            }
                else
                {
                    if (verifica_acesso("Vantagens", "Alterar"))
                    {
                        msg = _VantagemDao.update(_vantagem);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }                    
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                return Json(Url.Action("Index")); 
            }
            else
            {
                return Json(msg);
            }
        }

        [HttpPost]
        public ActionResult delete(int cod_vantagem)
        {
            if (verifica_acesso("Vantagens", "Deletar"))
            {
                VantagemDao _VantagemDao = new VantagemDao();
                return Json(_VantagemDao.delete(cod_vantagem));
            }
            return Json("Acesso não permitido");
        }

        public string limpar_list(string text)
        {
            if (text.Length > 0)
            {
                if (text.Substring(text.Length - 1) == "_" || text.Substring(text.Length - 1) == ";")
                {
                    return text.Substring(0, text.Length - 1);
                }
            }
            return text;
        }

        public string validar(Vantagem vantagem)
        {
            VantagemDao _VantagemDao = new VantagemDao();
            string msg = "";
            if (string.IsNullOrEmpty(vantagem.Descricao))
            {
                msg = "O Campo descrição é obrigatório.";
            }
            if (_VantagemDao.verificar_descricao(vantagem.Descricao, vantagem.Cod_Vantagem))
            {
                msg = "A Vantagem "+ vantagem.Descricao +" já existe.";
            }
            return msg;
        }
    }
}