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
            List<string> Classesload = new List<string>();
            
            AtributoDao _AtributoDao = new AtributoDao();
            ViewBag.Atributos = _AtributoDao.Listar_Atributos_ativo();
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            ClasseDao _ClasseDao = new ClasseDao();           

            List<Classe> listclas = _ClasseDao.Listar_Classes_dt_cb();
            ViewBag.Classes = listclas;

            if (id != 0)
            {
                _pericias = _PericiaDao.Listar_Pericia(id);

                foreach (int item in _pericias.requisito_classe)
                {
                    foreach (Classe _clas in listclas)
                    {
                        if (_clas.Cod_Classe == item)
                        {
                            Classesload.Add(_clas.Cod_Classe + "_" + _clas.Descricao);
                            break;
                        }
                    }
                }
            }
            else
            {
                _pericias.Ativo = true;
            }
            ViewBag.classesload = Classesload;
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

        [HttpPost]
        public ActionResult Adiciona(int Cod_Pericia, string Descricao, int Cod_Atributo, int penalidade_peso, string requisito_classe, bool Treinada, string Caracteristicas, int Campanha, bool Ativo)
        {
            Pericia _Pericia = new Pericia();
            _Pericia.Cod_Pericia = Cod_Pericia;
            _Pericia.Descricao = Descricao;
            _Pericia.Cod_Atributo = Cod_Atributo;
            _Pericia.penalidade_peso = penalidade_peso;
            if (string.IsNullOrEmpty(requisito_classe))
            {
                requisito_classe = "0";
            }
            _Pericia.requisito_classe = new List<int>(Array.ConvertAll(limpar_list(requisito_classe).Split('_'), int.Parse));
            _Pericia.Treinada = Treinada;
            _Pericia.Caracteristicas = Caracteristicas;
            _Pericia.Campanha = Campanha;
            _Pericia.Ativo = Ativo;

            string msg = validar(_Pericia);
            if (string.IsNullOrEmpty(msg))
            {
                PericiaDao _PericiaDao = new PericiaDao();
                if (_Pericia.Cod_Pericia == 0)
                {
                    if (verifica_acesso("Pericias", "Novo"))
                    {
                        msg = _PericiaDao.Insert(_Pericia);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }
                }
                else
                {
                    if (verifica_acesso("Pericias", "Alterar"))
                    {
                        msg = _PericiaDao.update(_Pericia);
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

        public string validar(Pericia pericia)
        {
            PericiaDao _PericiaDao = new PericiaDao();
            string msg = "";
            if (string.IsNullOrEmpty(pericia.Descricao))
            {
                msg = "O Campo descrição é obrigatório.";
            }
            if (_PericiaDao.verificar_descricao(pericia.Descricao, pericia.Cod_Pericia))
            {
                msg = "A Pericia " + pericia.Descricao + " já existe.";
            }
            return msg;
        }
    }
}