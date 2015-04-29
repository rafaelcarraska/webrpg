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
    public class RacaDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Raca> Listar_Racas_grid()
        {
            _conn = new Conexao();
            List<Raca> list_Raca = new List<Raca>();

            DataTable dt_raca = _conn.dataTable("select Cod_Raca, descricao, Custo from racas order by descricao", "RACA");
            foreach (DataRow row in dt_raca.Rows)
            {
                list_Raca.Add(new Raca
                {
                    Cod_Raca = Convert.ToInt32(row["Cod_Raca"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Custo = Convert.ToInt32(row["Custo"].ToString())
                });
            }

            return list_Raca;
        }

        public Raca Listar_Raca(int cod_raca)
        {
            _conn = new Conexao();
            Raca _Raca = new Raca();

            DataTable dt_raca = _conn.dataTable("select * from racas where cod_raca = "+ cod_raca +"", "RACA");
            if (dt_raca.Rows.Count >0)
	        {
                _Raca.Cod_Raca = Convert.ToInt32(dt_raca.Rows[0]["Cod_Raca"].ToString());
                _Raca.Descricao_Detalhada = dt_raca.Rows[0]["Descricao_Detalhada"].ToString();
                _Raca.Descricao = dt_raca.Rows[0]["Descricao"].ToString();
                _Raca.Campanha = Convert.ToInt32(dt_raca.Rows[0]["Campanha"].ToString());
                if (!string.IsNullOrEmpty(dt_raca.Rows[0]["Vantagens_Desvantagens"].ToString()))
                {
                    _Raca.Vantagens_Desvantagens = new List<int>(Array.ConvertAll(dt_raca.Rows[0]["Vantagens_Desvantagens"].ToString().Split('_'), int.Parse));
                }
                _Raca.Idiomas = dt_raca.Rows[0]["Idiomas"].ToString();
                _Raca.Pericias = new List<string>(dt_raca.Rows[0]["Pericias"].ToString().Split(';'));
                _Raca.Bonus_Atributo = new List<string>(dt_raca.Rows[0]["Bonus_Atributo"].ToString().Split(';'));
                _Raca.Lv_PontosPericias = Convert.ToDecimal(dt_raca.Rows[0]["Lv_PontosPericias"].ToString());
                _Raca.Lv_PontosVantagens = Convert.ToDecimal(dt_raca.Rows[0]["Lv_PontosVantagens"].ToString());
                _Raca.Lv_pontosAtributo = Convert.ToDecimal(dt_raca.Rows[0]["Lv_pontosAtributo"].ToString());
                _Raca.Custo = Convert.ToInt32(dt_raca.Rows[0]["Custo"].ToString());
                _Raca.Deslocamento = Convert.ToInt32(dt_raca.Rows[0]["Deslocamento"].ToString());
                _Raca.Bonus_CA = Convert.ToInt32(dt_raca.Rows[0]["Bonus_CA"].ToString());
                _Raca.Bonus_Hp = Convert.ToInt32(dt_raca.Rows[0]["Bonus_Hp"].ToString());
                _Raca.Bonus_Mp = Convert.ToInt32(dt_raca.Rows[0]["Bonus_Mp"].ToString());
                _Raca.Monstro = Convert.ToBoolean(dt_raca.Rows[0]["Monstro"].ToString());
                _Raca.Ativo = Convert.ToBoolean(dt_raca.Rows[0]["Ativo"].ToString());
            }

            return _Raca;
        }

        public string Insert(Raca raca)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strInsert = "insert into racas (Descricao_Detalhada, Descricao, Campanha, Vantagens_Desvantagens, Idiomas, Pericias, Lv_PontosPericias, "
                +"Lv_PontosVantagens, Custo, Bonus_Atributo, Deslocamento, Monstro, Ativo, Bonus_Hp, Bonus_Mp, Bonus_CA, Lv_pontosAtributo) "
                    + " values('" + raca.Descricao_Detalhada.Replace("'", "''") + "', '" + raca.Descricao.Replace("'", "''") + "', "+ raca.Campanha +", " 
                    + "'"+ string.Join<int>("_", raca.Vantagens_Desvantagens).Replace("'", "''") +"', '"+raca.Idiomas+"', '" + string.Join<string>(";", raca.Pericias).Replace("'", "''") + "', '"
                    + raca.Lv_PontosPericias.ToString().Replace(",", ".") + "', '" + raca.Lv_PontosVantagens.ToString().Replace(",", ".") + "', " + raca.Custo + ", '" + string.Join<string>(";", raca.Bonus_Atributo).Replace("'", "''") + "', "
                    + raca.Deslocamento + ", '" + raca.Monstro.ToString() + "',  '" + raca.Ativo.ToString() + "', " + raca.Bonus_Hp + ", " + raca.Bonus_Mp + ", " + raca.Bonus_CA + ", '"
                    + raca.Lv_pontosAtributo.ToString().Replace(",", ".") + "' )";
                _conn.execute(strInsert);
                _LogDao.insert("Raca", "add", "");
            }
            catch (Exception)
            {
                msg = "Erro ao adicionar a Raça ('" + raca.Descricao + "')";
            }
            return msg;
        }

        public string update(Raca raca)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strupdate = "update racas set Descricao_Detalhada = '" + raca.Descricao_Detalhada.Replace("'", "''") + "', Descricao = '" 
                    + raca.Descricao.Replace("'", "''") + "', Campanha = " + raca.Campanha + ", Vantagens_Desvantagens = '"
                    + string.Join<int>("_", raca.Vantagens_Desvantagens).Replace("'", "''") + "', Idiomas = '" + raca.Idiomas.Replace("'", "''") + "', Pericias = '"
                    + string.Join<string>(";", raca.Pericias).Replace("'", "''") + "', Lv_PontosPericias = '" + raca.Lv_PontosPericias.ToString().Replace(",", ".") + "', "
                + "Lv_PontosVantagens = '" + raca.Lv_PontosPericias.ToString().Replace(",", ".") + "', Custo = " + raca.Custo + ", Bonus_Atributo = '" 
                + string.Join<string>(";", raca.Bonus_Atributo).Replace("'", "''") + "', Deslocamento = "+raca.Deslocamento+", Monstro = '"
                +raca.Monstro.ToString()+"', Ativo = '"+raca.Ativo.ToString()+"', Bonus_Hp = "+raca.Bonus_Hp+", Bonus_Mp = "+raca.Bonus_Mp+", Bonus_CA = "
                + raca.Bonus_CA + ", Lv_pontosAtributo = '" + raca.Lv_pontosAtributo.ToString().Replace(",", ".") + "' where cod_raca = " + raca.Cod_Raca + " ";
                _conn.execute(strupdate);
                _LogDao.insert("Raca", "up", "cod_raca = " + raca.Cod_Raca.ToString());
            }
            catch (Exception)
            {
                msg = "Erro ao atualizar a Raça ('" + raca.Descricao + "')";
            }
            return msg;
        }

        public bool verificar_descricao(string descricao, int cod_raca)
        {
            try
            {
                _conn = new Conexao();

                string strselect = "select count(cod_raca) from racas where descricao = '" + descricao.Replace("'", "''") + "' and cod_raca <> " + cod_raca + "";
                if (Convert.ToInt32(_conn.scalar(strselect)) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return true;
            }
        }

        public string delete(int cod_raca)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strdelete = "delete from racas where cod_raca = " + cod_raca + "";
                _conn.execute(strdelete);
                _LogDao.insert("Raca", "del", "id " + cod_raca);
            }
            catch (Exception)
            {
                msg = "Erro ao deletar a Raça ";
            }
            return msg;
        }

    }
}
