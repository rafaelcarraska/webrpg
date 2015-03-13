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
    public class PericiaDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Pericia> Listar_Pericias_dt()
        {
            _conn = new Conexao();
            List<Pericia> list_Pericias = new List<Pericia>();

            DataTable dt_Pericias = _conn.dataTable("select cod_pericia, descricao, ativo from pericias order by descricao", "PERICIA");
            foreach (DataRow row in dt_Pericias.Rows)
            {
                list_Pericias.Add(new Pericia
                {
                    Cod_Pericia = Convert.ToInt32(row["Cod_Pericia"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Ativo = Convert.ToBoolean(row["ativo"].ToString())
                });
            }

            return list_Pericias;
        }

        public List<Pericia> Listar_Pericias_dt_cb()
        {
            _conn = new Conexao();
            List<Pericia> list_Pericias = new List<Pericia>();

            DataTable dt_Pericia = _conn.dataTable("select cod_pericia, descricao from pericias where ativo = 1 order by descricao", "PERICIA");
            foreach (DataRow row in dt_Pericia.Rows)
            {
                list_Pericias.Add(new Pericia
                {
                    Cod_Pericia = Convert.ToInt32(row["Cod_Pericia"].ToString()),
                    Descricao = row["descricao"].ToString()
                });
            }

            return list_Pericias;
        }

        public Pericia Listar_Pericia(int cod_pericia)
        {
            _conn = new Conexao();
            Pericia _Pericia = new Pericia();

            DataTable dt_pericia = _conn.dataTable("select * from Pericias where cod_pericia = " + cod_pericia + "", "PERICIA");
            if (dt_pericia.Rows.Count > 0)
            {
                _Pericia.Cod_Pericia = Convert.ToInt32(dt_pericia.Rows[0]["Cod_Pericia"].ToString());
                _Pericia.Descricao = dt_pericia.Rows[0]["Descricao"].ToString();
                _Pericia.Cod_Atributo = Convert.ToInt32(dt_pericia.Rows[0]["Cod_Atributo"].ToString());
                _Pericia.penalidade_peso = Convert.ToInt32(dt_pericia.Rows[0]["penalidade_peso"].ToString());
                _Pericia.Campanha = Convert.ToInt32(dt_pericia.Rows[0]["Campanha"].ToString());
                _Pericia.requisito_classe = new List<int>(Array.ConvertAll(dt_pericia.Rows[0]["requisito_classe"].ToString().Split('_'), int.Parse));
                _Pericia.Treinada = Convert.ToBoolean(dt_pericia.Rows[0]["Treinada"].ToString());
                _Pericia.Caracteristicas = dt_pericia.Rows[0]["Caracteristicas"].ToString();
                _Pericia.Campanha = Convert.ToInt32(dt_pericia.Rows[0]["Campanha"].ToString());
                _Pericia.Ativo = Convert.ToBoolean(dt_pericia.Rows[0]["Ativo"].ToString());
            }

            return _Pericia;
        }

        public string Insert(Pericia pericia)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strInsert = "insert into vantagens (Descricao, Custo, Bonus_Atributo, Pre_Vantagens, Pre_Requisitos, Caracteristicas, Campanha, Ativo) "
                    + " values('" + vantagem.Descricao.Replace("'", "''") + "', " + vantagem.Custo + ", '" + string.Join<string>(";", vantagem.Bonus_Atributo).Replace("'", "''") + "', '"
                    + string.Join<int>("_", vantagem.Pre_Vantagens).Replace("'", "''") + "', '" + vantagem.Pre_Requisitos.Replace("'", "''") + "', '" + vantagem.Caracteristicas.Replace("'", "''") + "', "
                    + vantagem.Campanha + ", '" + vantagem.Ativo.ToString() + "')";
                _conn.execute(strInsert);
                _LogDao.insert("Vantagem", "add", "");
            }
            catch (Exception)
            {
                msg = "Erro ao adicionar a Vantagem ('" + pericia.Descricao + "')";
            }
            return msg;
        }

        public string update(Vantagem vantagem)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strupdate = "update vantagens set Descricao = '" + vantagem.Descricao.Replace("'", "''") + "', Custo = " + vantagem.Custo
                    + ", Bonus_Atributo = '" + string.Join<string>(";", vantagem.Bonus_Atributo).Replace("'", "''") + "', Pre_Vantagens = '" + string.Join<int>("_", vantagem.Pre_Vantagens).Replace("'", "''")
                    + "', Pre_Requisitos = '" + vantagem.Pre_Requisitos.Replace("'", "''") + "', Caracteristicas = '" + vantagem.Caracteristicas.Replace("'", "''") + "', Campanha = " + vantagem.Campanha + ", Ativo = '" + vantagem.Ativo.ToString() + "') ";
                _conn.execute(strupdate);
                _LogDao.insert("Vantagem", "up", "cod_vantagem = " + vantagem.Cod_Vantagem.ToString());
            }
            catch (Exception)
            {
                msg = "Erro ao atualizar a Vantagem ('" + vantagem.Descricao + "')";
            }
            return msg;
        }

        public bool verificar_descricao(string descricao, int cod_pericia)
        {
            try
            {
                _conn = new Conexao();

                string strselect = "select count(cod_pericia) from pericias where descricao = '" + descricao.Replace("'", "''") + "' and cod_pericia <> " + cod_pericia + "";
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

        public string delete(int cod_pericia)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strdelete = "delete from Pericia where cod_pericia = " + cod_pericia + "";
                _conn.execute(strdelete);
                _LogDao.insert("Pericia", "del", "id " + cod_pericia);
            }
            catch (Exception)
            {
                msg = "Erro ao deletar a Pericia ";
            }
            return msg;
        }

    }
}
