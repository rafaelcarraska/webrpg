using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using rpg.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace rpg.Dao
{
    public class ItemDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Item> Listar_Itens_dt()
        {
            _conn = new Conexao();
            List<Item> list_Itens = new List<Item>();

            DataTable dt_Item = _conn.dataTable("select Cod_Item, Descricao, Valor_Min, Valor_Max, Descricao_Detalhada, Ativo from Itens order by descricao", "ITEM");
            foreach (DataRow row in dt_Item.Rows)
            {
                list_Itens.Add(new Item
                {
                    Cod_Item = Convert.ToInt32(row["Cod_Item"].ToString()),
                    Descricao = row["Descricao"].ToString(),
                    Valor_Min = Convert.ToDecimal(row["Valor_Min"].ToString()),
                    Valor_Max = Convert.ToDecimal(row["Valor_Max"].ToString()),
                    Descricao_Detalhada = row["Descricao_Detalhada"].ToString(),
                    Ativo = Convert.ToBoolean(row["Ativo"].ToString())
                });
            }

            return list_Itens;
        }

        public Item Listar_Item(int cod_Item)
        {
            _conn = new Conexao();
            Item _Item = new Item();

            DataTable dt_item = _conn.dataTable("select * from Itens where cod_Item = " + cod_Item + "", "ITEM");
            if (dt_item.Rows.Count > 0)
            {
                _Item.Cod_Item = Convert.ToInt32(dt_item.Rows[0]["Cod_Item"].ToString());
                _Item.Descricao = dt_item.Rows[0]["Descricao"].ToString();
                _Item.Valor_Min = Convert.ToDecimal(dt_item.Rows[0]["Valor_Min"].ToString());
                _Item.Valor_Max = Convert.ToDecimal(dt_item.Rows[0]["Valor_Max"].ToString());
                _Item.Up = Convert.ToBoolean(dt_item.Rows[0]["Up"].ToString());
                _Item.Bonus_Atk_Corpo = Convert.ToInt32(dt_item.Rows[0]["Bonus_Atk_Corpo"].ToString());
                _Item.Bonus_Atk_Distanc = Convert.ToInt32(dt_item.Rows[0]["Bonus_Atk_Distanc"].ToString());
                _Item.Decisivo = Convert.ToInt32(dt_item.Rows[0]["Decisivo"].ToString());
                _Item.Critico = Convert.ToInt32(dt_item.Rows[0]["Critico"].ToString());
                _Item.Dano = dt_item.Rows[0]["Dano"].ToString();
                _Item.Resistencia = Convert.ToInt32(dt_item.Rows[0]["Resistencia"].ToString());
                _Item.Peso = Convert.ToDecimal(dt_item.Rows[0]["Peso"].ToString());
                _Item.Ca = Convert.ToInt32(dt_item.Rows[0]["Ca"].ToString());
                _Item.Raridade = Convert.ToInt32(dt_item.Rows[0]["Raridade"].ToString());
                _Item.Tipo = dt_item.Rows[0]["Tipo"].ToString();
                if (string.IsNullOrEmpty(dt_item.Rows[0]["Pre_Requisito"].ToString()))
                {
                    _Item.Pre_Requisito = new List<int>();
                }
                else
                {
                    _Item.Pre_Requisito = new List<int>(Array.ConvertAll(dt_item.Rows[0]["Pre_Requisito"].ToString().Split('_'), int.Parse));
                }
                _Item.Penalidade = dt_item.Rows[0]["Penalidade"].ToString();
                _Item.Duas_Maos = Convert.ToBoolean(dt_item.Rows[0]["Duas_Maos"].ToString());
                _Item.Municao = Convert.ToBoolean(dt_item.Rows[0]["Municao"].ToString());
                _Item.Descricao_Detalhada = dt_item.Rows[0]["Descricao_Detalhada"].ToString();
                _Item.Campanha = Convert.ToInt32(dt_item.Rows[0]["Campanha"].ToString());
                _Item.Ativo = Convert.ToBoolean(dt_item.Rows[0]["Ativo"].ToString());
            }

            return _Item;
        }

        public string Insert(Item item)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strInsert = "insert into itens (Descricao, Valor_Min, Valor_Max, Up, Bonus_Atk_Corpo, Bonus_Atk_Distanc, Decisivo, Critico, Resistencia, Peso, Ca, Raridade, Tipo, Pre_Requisito, Penalidade, Duas_Maos, Municao, Descricao_Detalhada, Ativo, Campanha, Dano) "
                    + " values('" + item.Descricao.Replace("'", "''") + "', " + item.Valor_Min.ToString().Replace(",", ".") + ", " + item.Valor_Max.ToString().Replace(",", ".") + ", '" + item.Up + "', " + item.Bonus_Atk_Corpo + ", " + item.Bonus_Atk_Distanc + ", " + item.Decisivo + ", " + item.Critico + ", " + item.Resistencia + ", " + item.Peso.ToString().Replace(",", ".") + ", " + item.Ca + ", " + item.Raridade + ", '" + item.Tipo + "', '" + string.Join<int>("_", item.Pre_Requisito).Replace("'", "''") + "', " + item.Penalidade + ", '" + item.Duas_Maos.ToString() + "', '" + item.Municao.ToString() + "', '" + item.Descricao_Detalhada.Replace("'", "''") + "', '" + item.Ativo.ToString() + "', " + item.Campanha + ", '" + item.Dano + "')";
                _conn.execute(strInsert);
                _LogDao.insert("Item", "add", "");
            }
            catch (Exception)
            {
                msg = "Erro ao adicionar o Item ('" + item.Descricao + "')";
            }
            return msg;
        }

        public string update(Item item)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strupdate = "update itens set Descricao = '" + item.Descricao.Replace("'", "''") + "', Valor_Min = " + item.Valor_Min.ToString().Replace(",", ".") + ", Valor_Max = " + item.Valor_Max.ToString().Replace(",", ".") + ", Up = '" + item.Up + "', Bonus_Atk_Corpo = " + item.Bonus_Atk_Corpo + ", Bonus_Atk_Distanc = " + item.Bonus_Atk_Distanc + ", Decisivo = " + item.Decisivo + ", Critico = " + item.Critico + ", Resistencia = " + item.Resistencia + ", Peso = " + item.Peso.ToString().Replace(",", ".") + ", Ca = " + item.Ca + ", Raridade = " + item.Raridade + ", Tipo = '" + item.Tipo + "', Pre_Requisito = '" + string.Join<int>("_", item.Pre_Requisito).Replace("'", "''") + "', Penalidade = " + item.Penalidade + ", Duas_Maos = '" + item.Duas_Maos.ToString() + "', Municao = '" + item.Municao.ToString() + "', Descricao_Detalhada = '" + item.Descricao_Detalhada.Replace("'", "''") + "', Ativo = '" + item.Ativo.ToString() + "', Campanha = " + item.Campanha + ", Dano = '" + item.Dano + "' where Cod_Item = " + item.Cod_Item + " ";
                _conn.execute(strupdate);
                _LogDao.insert("Item", "up", "cod_Item = " + item.Cod_Item.ToString());
            }
            catch (Exception)
            {
                msg = "Erro ao atualizar a Item ('" + item.Descricao + "')";
            }
            return msg;
        }


        public string delete(int cod_item)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strdelete = "delete from itens where cod_item = " + cod_item + "";
                _conn.execute(strdelete);
                _LogDao.insert("Item", "del", "id " + cod_item);
            }
            catch (Exception)
            {
                msg = "Erro ao deletar o Item ";
            }
            return msg;
        }
    }
}
