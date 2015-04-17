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
        [Route("Classes", Name = "Classes")] 
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
            Classe _Classe = new Classe();

            
            VantagemDao _VantagemDao = new VantagemDao();
            PericiaDao _periciaDao = new PericiaDao();
            Vantagem _Vantagens = new Vantagem();
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            List<Vantagem> listvant = _VantagemDao.Listar_Vantagens_dt_cb();
            List<Pericia> listpericia = _periciaDao.Listar_Pericias_dt_cb();
            ViewBag.Vantagens = listvant;
            ViewBag.Pericias = listpericia;

            List<string> vantagensload = new List<string>();
            List<string> periciasload = new List<string>();
            if (id != 0)
            {
                _Classe = _ClasseDao.Listar_Classe(id);
                if (_Classe.Cod_Classe == 0)
                {
                    return RedirectToAction("Index", "Itens");
                }
                if (_Classe.Vantagens_Desvantagens != null)
                {
                    foreach (int classe in _Classe.Vantagens_Desvantagens)
                    {
                        foreach (Vantagem van in listvant)
                        {
                            if (van.Cod_Vantagem == classe)
                            {
                                vantagensload.Add(van.Cod_Vantagem + "_" + van.Descricao);
                                break;
                            }
                        }
                    }
                }
                if (_Classe.Pericias != null)
                {

                    foreach (string classe in _Classe.Pericias)
                    {
                        List<string> b_A = new List<string>(classe.Split('_'));
                        foreach (Pericia per in listpericia)
                        {
                            if (per.Cod_Pericia.ToString() == b_A[0])
                            {
                                periciasload.Add(b_A[0] + "_" + b_A[1] + "_" + per.Descricao);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                _Classe.Ativo = true;
            }           
            
            ViewBag.vantagensload = vantagensload;
            ViewBag.periciasload = periciasload;

            return View(_Classe);
        }

        [HttpPost]
        public ActionResult Adiciona(int Cod_Classe, string Descricao, int Custo, string Pericias, string Vantagens_Desvantagens, string Descricao_Detalhada, int Campanha, bool Ativo)
        {
            Classe _Classe = new Classe();
            _Classe.Cod_Classe = Cod_Classe;
            _Classe.Descricao = Descricao;
            _Classe.Custo = Custo;
            _Classe.Pericias = new List<string>(limpar_list(Pericias).Split(';'));
            if (string.IsNullOrEmpty(Vantagens_Desvantagens))
            {
                Vantagens_Desvantagens = "0";
            }
            _Classe.Vantagens_Desvantagens = new List<int>(Array.ConvertAll(limpar_list(Vantagens_Desvantagens).Split('_'), int.Parse));
            _Classe.Descricao_Detalhada = Descricao_Detalhada;
            _Classe.Campanha = Campanha;
            _Classe.Ativo = Ativo;

            string msg = validar(_Classe);
            if (string.IsNullOrEmpty(msg))
            {
                ClasseDao _ClasseDao = new ClasseDao();
                if (_Classe.Cod_Classe == 0)
                {
                    if (verifica_acesso("Classes", "Novo"))
                    {
                        msg = _ClasseDao.Insert(_Classe);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }
                }
                else    
                {
                    if (verifica_acesso("Classes", "Alterar"))
                    {
                        msg = _ClasseDao.update(_Classe);
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
        public ActionResult delete(int cod_classe)
        {
            if (verifica_acesso("Classes", "Deletar"))
            {
                ClasseDao _ClasseDao = new ClasseDao();
                return Json(_ClasseDao.delete(cod_classe));
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

        public string validar(Classe classe)
        {
            ClasseDao _ClasseDao = new ClasseDao();
            string msg = "";
            if (string.IsNullOrEmpty(classe.Descricao))
            {
                msg = "O Campo descrição é obrigatório.";
            }
            if (_ClasseDao.verificar_descricao(classe.Descricao, classe.Cod_Classe))
            {
                msg = "A Classe " + classe.Descricao + " já existe.";
            }
            return msg;
        }
    }
}