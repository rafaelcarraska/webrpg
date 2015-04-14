using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;


namespace rpg.Controllers
{
    public class ItensController : BaseController
    {
        // GET: Item
        public ActionResult Index()
        {
            if (!verifica_acesso("Itens", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Itens";
            ItemDao _ItemDao = new ItemDao();
            IList<Item> _Itens = _ItemDao.Listar_Itens_dt();
            return View(_Itens);
        }

        [Route("Item/{id}", Name = "Editar_Item")]
        public ActionResult Form(int id)
        {
            if (!verifica_acesso("Itens", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.pagina = "Item / Detalhes";

            ItemDao _ItemDao = new ItemDao();
            Item _Item = new Item();
            VantagemDao _VantagemDao = new VantagemDao();
            Vantagem _Vantagens = new Vantagem();
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.Campanhas = _CampanhaDao.Listar_Campanhas_cb_mestre();
            List<Vantagem> listvant = _VantagemDao.Listar_Vantagens_dt_cb();

            List<List<string>> _tipo = new List<List<string>>();
            List<string> _row = new List<string>();
            _row.Add("C");
            _row.Add("Comum");
            _tipo.Add(_row);
            _row.Clear();
            _row.Add("A");
            _row.Add("Ataque");
            _tipo.Add(_row);
            _row.Clear();
            _row.Add("D");
            _row.Add("Defesa");
            _tipo.Add(_row);
            ViewBag.Tipo = _tipo;

            ViewBag.Vantagens = listvant;
            if (id != 0)
            {
                _Item = _ItemDao.Listar_Item(id);
                if (_Item.Cod_Item == 0)
                {
                    return RedirectToAction("Index", "Itens");
                }                
            }

            List<string> vantagensload = new List<string>();
            if (_Item.Pre_Requisito != null)
            {
                foreach (int item in _Item.Pre_Requisito)
                {
                    foreach (Vantagem van in listvant)
                    {
                        if (van.Cod_Vantagem == item)
                        {
                            vantagensload.Add(van.Cod_Vantagem + "_" + van.Descricao);
                            break;
                        }
                    }
                }
            }
            ViewBag.vantagensload = vantagensload;

            return View(_Item);
        }

        [HttpPost]
        public ActionResult Adiciona(int Cod_Item, string Descricao, decimal Valor_Min, decimal Valor_Max, bool Up, int Bonus_Atk_Corpo, int Bonus_Atk_Distanc, int Decisivo, int Critico, int Resistencia, decimal	Peso, int Ca, int Raridade, string Tipo, string Pre_Requisito, string Penalidade, bool Duas_Maos, bool Municao, string Descricao_Detalhada, bool Ativo, int Campanha, string Dano )
        {
            Item _Item = new Item();

            _Item.Cod_Item = Cod_Item; 
            _Item.Descricao = Descricao;
            _Item.Valor_Min = Valor_Min;
            _Item.Valor_Max = Valor_Max;
            _Item.Up = Up;
            _Item.Bonus_Atk_Corpo = Bonus_Atk_Corpo;
            _Item.Bonus_Atk_Distanc = Bonus_Atk_Distanc;
            _Item.Decisivo = Decisivo;
            _Item.Critico = Critico;
            _Item.Dano = Dano;
            _Item.Resistencia = Resistencia;
            _Item.Peso = Peso;
            _Item.Ca = Ca;
            _Item.Raridade = Raridade;
            _Item.Tipo = Tipo;
            if (string.IsNullOrEmpty(Pre_Requisito))
            {
                Pre_Requisito = "0";
            }
            _Item.Pre_Requisito = new List<int>(Array.ConvertAll(limpar_list(Pre_Requisito).Split('_'), int.Parse));
            _Item.Penalidade = Penalidade;
            _Item.Duas_Maos = Duas_Maos;
            _Item.Municao = Municao;
            _Item.Descricao_Detalhada = Descricao_Detalhada;
            _Item.Ativo = Ativo;
            _Item.Campanha = Campanha; 

            string msg = validar(_Item);
            if (string.IsNullOrEmpty(msg))
            {
                ItemDao _ItemDao = new ItemDao();
                if (_Item.Cod_Item == 0)
                {
                    if (verifica_acesso("Itens", "Alterar"))
                    {
                        msg = _ItemDao.Insert(_Item);
                    }
                    else
                    {
                        msg = "Acesso não permitido";
                    }
                }
                else
                {
                    if (verifica_acesso("Itens", "Alterar"))
                    {
                        msg = _ItemDao.update(_Item);
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
        public ActionResult delete(int cod_item)
        {
            if (verifica_acesso("Itens", "Deletar"))
            {
                ItemDao _ItemDao = new ItemDao();
                return Json(_ItemDao.delete(cod_item));
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

        public string validar(Item Item)
        {
            VantagemDao _VantagemDao = new VantagemDao();
            string msg = "";
            if (string.IsNullOrEmpty(Item.Descricao))
            {
                msg = "O Campo descrição é obrigatório.";
            }
            if (_VantagemDao.verificar_descricao(Item.Descricao, Item.Cod_Item))
            {
                msg = "O Item "+ Item.Descricao +" já existe.";
            }
            return msg;
        }
    
    }
}