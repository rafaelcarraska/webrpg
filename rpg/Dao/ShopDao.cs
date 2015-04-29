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
    public class ShopDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Shop> Listar_Shop_dt()
        {
            _conn = new Conexao();
            List<Shop> list_shop = new List<Shop>();

            DataTable dt_shop = _conn.dataTable("select Cod_Shop, Descricao, Valor_Min, Valor_Max, Magico, Raridade, N_Max from Shop order by descricao", "SHOP");
            foreach (DataRow row in dt_shop.Rows)
            {
                list_shop.Add(new Shop
                {
                    Cod_Shop = Convert.ToInt32(row["Cod_Shop"].ToString()),
                    Descricao = row["Descricao"].ToString(),
                    Valor_Min = Convert.ToDecimal(row["Valor_Min"].ToString()),
                    Valor_Max = Convert.ToDecimal(row["Valor_Max"].ToString()),
                    Magico = Convert.ToBoolean(row["Magico"].ToString()),
                    Raridade = Convert.ToInt32(row["Raridade"].ToString()),
                    N_Max = Convert.ToInt32(row["N_Max"].ToString())
                });
            }

            return list_shop;
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
