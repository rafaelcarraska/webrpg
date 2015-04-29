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
       [Route("Racas", Name = "Racas")] 
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

        [Route("Raca/{id}", Name="Editar_Raca")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("Racas", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Raçaas / Detalhes";
            RacaDao _RacaDao = new RacaDao();
            Raca _Raca = new Raca();


            VantagemDao _VantagemDao = new VantagemDao();
            PericiaDao _periciaDao = new PericiaDao();
            AtributoDao _AtributoDao = new AtributoDao();
            List<Atributos> listatributos = _AtributoDao.Listar_Atributos_ativo();
            ViewBag.Atributos = listatributos;
            Vantagem _Vantagens = new Vantagem();
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            List<Vantagem> listvant = _VantagemDao.Listar_Vantagens_dt_cb();
            List<Pericia> listpericia = _periciaDao.Listar_Pericias_dt_cb();
            ViewBag.Vantagens = listvant;
            ViewBag.Pericias = listpericia;

            List<string> vantagensload = new List<string>();
            List<string> periciasload = new List<string>();
            List<string> atributosload = new List<string>();
            if (id != 0)
            {
                _Raca = _RacaDao.Listar_Raca(id);
                if (_Raca.Cod_Raca == 0)
                {
                    return RedirectToAction("Index", "Itens");
                }
                if (_Raca.Vantagens_Desvantagens != null)
                {
                    foreach (int raca in _Raca.Vantagens_Desvantagens)
                    {
                        foreach (Vantagem van in listvant)
                        {
                            if (van.Cod_Vantagem == raca)
                            {
                                vantagensload.Add(van.Cod_Vantagem + "_" + van.Descricao);
                                break;
                            }
                        }
                    }
                }
                if (_Raca.Pericias != null)
                {
                    foreach (string raca in _Raca.Pericias)
                    {
                        List<string> b_A = new List<string>(raca.Split('_'));
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
                if (_Raca.Bonus_Atributo != null)
                {
                    foreach (string item in _Raca.Bonus_Atributo)
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
                }

            }
            else
            {
                _Raca.Ativo = true;
            }

            ViewBag.vantagensload = vantagensload;
            ViewBag.periciasload = periciasload;
            ViewBag.atributosload = atributosload;

            return View(_Raca);
        }

        public ActionResult Adiciona(int Cod_Raca, string Descricao_Detalhada, string Descricao, int Campanha, string Vantagens_Desvantagens, string Idiomas, string Pericias, decimal Lv_PontosPericias, decimal Lv_PontosVantagens, int Custo, string Bonus_Atributo, int Deslocamento, bool Monstro, bool Ativo, int Bonus_Hp, int Bonus_Mp, int Bonus_CA, decimal Lv_pontosAtributo)
        {
            Raca _Raca = new Raca();
            _Raca.Cod_Raca = Cod_Raca;
            _Raca.Descricao_Detalhada = Descricao_Detalhada;
            _Raca.Descricao = Descricao;
            _Raca.Campanha = Campanha;
            if (string.IsNullOrEmpty(Vantagens_Desvantagens))
            {
                Vantagens_Desvantagens = "0";
            }
            _Raca.Vantagens_Desvantagens = new List<int>(Array.ConvertAll(limpar_list(Vantagens_Desvantagens).Split('_'), int.Parse));
            _Raca.Idiomas = Idiomas;
            _Raca.Pericias = new List<string>(limpar_list(Pericias).Split(';'));
            _Raca.Lv_PontosPericias = Lv_PontosPericias;
            _Raca.Lv_PontosVantagens = Lv_PontosVantagens;
            _Raca.Custo = Custo;
            _Raca.Bonus_Atributo = new List<string>(limpar_list(Bonus_Atributo).Split(';'));
            _Raca.Deslocamento = Deslocamento;
            _Raca.Monstro = Monstro;
            _Raca.Ativo = Ativo;
            _Raca.Bonus_Hp = Bonus_Hp;
            _Raca.Bonus_Mp = Bonus_Mp;
            _Raca.Bonus_CA = Bonus_CA;
            _Raca.Lv_pontosAtributo = Lv_pontosAtributo;

            string msg = validar(_Raca);
            if (string.IsNullOrEmpty(msg))
            {
                RacaDao _RacaDao = new RacaDao();
                if (_Raca.Cod_Raca == 0)
                {
                    if (verifica_acesso("Racas", "Novo"))
                    {
                        msg = _RacaDao.Insert(_Raca);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }
                }
                else
                {
                    if (verifica_acesso("Racas", "Alterar"))
                    {
                        msg = _RacaDao.update(_Raca);
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
        public ActionResult delete(int cod_raca)
        {
            if (verifica_acesso("Raça", "Deletar"))
            {
                RacaDao _RacaDao = new RacaDao();
                return Json(_RacaDao.delete(cod_raca));
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

        public string validar(Raca raca)
        {
            RacaDao _RacaDao = new RacaDao();
            string msg = "";
            if (string.IsNullOrEmpty(raca.Descricao))
            {
                msg = "O Campo descrição é obrigatório.";
            }
            if (_RacaDao.verificar_descricao(raca.Descricao, raca.Cod_Raca))
            {
                msg = "A Raça " + raca.Descricao + " já existe.";
            }
            return msg;
        }
    }
}